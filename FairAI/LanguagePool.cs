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
            var disp = 2.0;
            var dmX = dm.HistoryValue;
            var dmY = dm.DepthValue;
            var str = "";
            DataModel closestObject;
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
                    Math.Abs(x.DepthValue - dmY)
                );
                }
                dmX = (closestObject.DepthValue + dmX) / 2;
                dmY = (closestObject.DepthValue + dmY) / 2;
                flag = !flag;
                var pre = (Math.Abs(dmX - dm.DepthValue) + Math.Abs(dmY - dm.HistoryValue));
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
    }
}