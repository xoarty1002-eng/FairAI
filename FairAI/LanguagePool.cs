using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace FairAI
{
    public class LanguagePool : ILanguage
    {
        public List<DataModel> Data = [];

        public void Add(string Word)
        {
            var r = new Random();
            if (Data.FirstOrDefault(a => a.Word == Word) == default(DataModel))
            {
                Data.Add(new DataModel { Word = Word, DepthValue = r.NextDouble(), HistoryValue = r.NextDouble() });
            }
        }

        public StateModel Calculate(string request)
        {
            var ret = new StateModel();
            var dataArray = request.Split(" ");
            foreach (var element in dataArray)
            {
                Add(element);
                var e = Data.FirstOrDefault(a => a.Word == element);
                ret.DepthValue = (ret.DepthValue + e.DepthValue) / 2;
                ret.HistoryValue = (ret.HistoryValue + e.HistoryValue) / 2;
            }
            return ret;
        }


        public string Generate(StateModel dm)
        {
            if (Data == null || !Data.Any()) return string.Empty;

            // 1. Establish the two baseline target values from your StateModel properties
            double targetFirst = dm.HistoryValue;
            double targetSecond = dm.DepthValue;

            string generatedSentence = "";
            var visited = new HashSet<DataModel>();

            // True = look for closest to First, False = look for closest to Second
            bool targetToggle = true;

            while (true)
            {
                DataModel closestObject = null;

                if (targetToggle)
                {
                    // 2. Find closest value to the FIRST target
                    closestObject = Data
                        .Where(x => !visited.Contains(x))
                        .MinBy(x => Math.Abs(x.HistoryValue - targetFirst));

                    if (closestObject == null) break;

                    // Calculate missing value (error distance) to First
                    double missingValue = targetFirst - closestObject.HistoryValue;

                    // Add missing value adjustment to the SECOND target
                    targetSecond += missingValue;
                }
                else
                {
                    // 3. Find closest value to the SECOND target
                    closestObject = Data
                        .Where(x => !visited.Contains(x))
                        .MinBy(x => Math.Abs(x.DepthValue - targetSecond));

                    if (closestObject == null) break;

                    // Calculate missing value (error distance) to Second
                    double missingValue = targetSecond - closestObject.DepthValue;

                    // Add missing value adjustment back to the FIRST target
                    targetFirst += missingValue;
                }

                // 4. Record the word and lock the data node from repeating
                visited.Add(closestObject);
                generatedSentence += closestObject.Word + " ";

                // 5. Flip the flag to alternate targets on the next loop iteration
                targetToggle = !targetToggle;
            }

            return generatedSentence.Trim();
        }
    }

    /*       public string Generate(StateModel dm)
           {
               var disp = 2.0;
               var str = "";
               while (true)
               {
                   var closestObject = Data.MinBy(x => Math.Abs(Math.Abs(x.HistoryValue - x.DepthValue) - disp));
                   var pre = Math.Abs(closestObject.HistoryValue + closestObject.DepthValue - dm.HistoryValue+dm.DepthValue);
                   if (pre < disp)
                   {
                       disp = pre;
                       str += closestObject.Word + " ";
                   }
                   else
                   {
                       break;
                   }
               }
               return str;
           }
     */
}