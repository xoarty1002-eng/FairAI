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

        public void Add(string firstWord, string lastWord, double? meaningValue)
        {
            var r = new Random();
            if (meaningValue == null) 
            {
                meaningValue = r.NextDouble();
            }
            if (Data.FirstOrDefault(a => a.FirstWord == firstWord && a.LastWord == lastWord) == default(TextModel))
            {
                Data.Add(new TextModel { FirstWord = firstWord, LastWord = lastWord, MeaningValue = (double)meaningValue , HistoryValue = r.NextDouble()});
            }
        }

        public LanguageModel Calculate(string request)
        {
            var ret = new LanguageModel();
            var dataArray = request.Split(" ");
            for (var i = 0; i< dataArray.Count()-1; i++)
            {
                Add(dataArray[i], dataArray[i+1], null);
                var e = Data.FirstOrDefault(a => a.FirstWord == dataArray[i] && a.LastWord == dataArray[i+1]);
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
            var flag = false;
            var wordCount = 0;
            var lastWord = "";
            var firstWord = "";
            while (true)
            {
                if (flag)
                {
                    closestObject = Data.Where(a => string.IsNullOrEmpty(lastWord) || a.FirstWord == lastWord).MinBy(x => Math.Abs(x.HistoryValue - dmX));
                    if (closestObject == null) 
                    {
                        Add("FairAI", lastWord, disp);
                        return str;
                    }
                    lastWord = closestObject.LastWord;
                }
                else
                {
                    closestObject = Data.Where(a => string.IsNullOrEmpty(lastWord) || a.FirstWord == lastWord).MinBy(x => Math.Abs(x.MeaningValue - dmY));
                    if (closestObject == null)
                    {
                        Add("FairAI", lastWord, disp);
                        return str;
                    }
                    lastWord = closestObject.LastWord;
                }
                dmX = (closestObject.MeaningValue + dmX) / 2;
                dmY = (closestObject.MeaningValue + dmY) / 2;
                flag = !flag;
                var pre = (Math.Abs(dmX - dm.MeaningValue) + Math.Abs(dmY - dm.HistoryValue));
                if (pre < disp)
                {
                    disp = pre;
                    if (firstWord != lastWord) 
                    {
                        firstWord = closestObject.FirstWord;
                        str += firstWord + " ";
                    }
                    else 
                    {
                        Add("FairAI", lastWord, disp);
                        return str;
                    }
                    wordCount++;
                }
                else
                {
                    if (wordCount == 0)
                    {
                        break;
                    }
                    else
                    {
                        wordCount--;
                        Data.Remove(closestObject);
                        Add(closestObject.FirstWord, closestObject.LastWord, closestObject.HistoryValue);
                    }
                }
            }
            Add("FairAI", lastWord, disp);
            return str;
        }
    }
}