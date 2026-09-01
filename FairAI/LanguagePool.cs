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
            if (Data.FirstOrDefault(a=> a.Word == Word)==default(DataModel))
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
            var disp = dm.HistoryValue+dm.DepthValue;
            var str = "";
            while (true)
            {
                var closestObject = Data.MinBy(x => Math.Abs(x.HistoryValue + x.DepthValue - disp));
                var pre = closestObject.HistoryValue + closestObject.DepthValue;
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
