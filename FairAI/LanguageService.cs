using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace FairAI
{
    public class LanguageService : ILanguage
    {
        public List<TextModel> Data { get; set; }
        public LanguageService() 
        {
            Data = new List<TextModel>();
        }

        public void Add(string Word)
        {
            var r = new Random();
            if (Data.FirstOrDefault(a => a.TextValue == Word) == default(TextModel))
            {
                Data.Add(new TextModel { TextValue = Word, MeaningValue = r.NextDouble(), HistoryValue = r.NextDouble() });
            }
        }

        public LanguageModel Calculate(string request)
        {
            var ret = new LanguageModel();
            var dataArray = request.Split(" ");
            foreach (var element in dataArray)
            {
                Add(element);
                var e = Data.FirstOrDefault(a => a.TextValue == element);
                ret.MeaningValue = (ret.MeaningValue + e.MeaningValue) / 2;
                ret.HistoryValue = (ret.HistoryValue + e.HistoryValue) / 2;
            }
            return ret;
        }

        public string Generate(LanguageModel dm)
        {
            var disp = 2.0;
            var dmX = dm.HistoryValue;
            var dmY = dm.MeaningValue;
            var str = "";
            TextModel closestObject;
            var flag = true;
            while (true)
            {
                if (flag)
                {
                    closestObject = Data.MinBy(x =>
                        Math.Abs(x.HistoryValue - dmX)
                );
                }
                else
                {
                    closestObject = Data.MinBy(x =>
                    Math.Abs(x.MeaningValue - dmY)
                );
                }
                dmX = (closestObject.MeaningValue + dmX) / 2;
                dmY = (closestObject.MeaningValue + dmY) / 2;
                flag = !flag;
                var pre = (Math.Abs(dmX - dm.MeaningValue) + Math.Abs(dmY - dm.HistoryValue));
                if (pre < disp)
                {
                    disp = pre;
                    str += closestObject.TextValue + " ";
                }
                else
                {
                    break;
                }
            }
            return str;
        }
    }
}