using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FairAI
{
    public class ArtificialService : IDepth
    {
        public List<NeuronModel> Pool { get; set; }

        public ArtificialService(int lenght)
        {
            Pool = new List<NeuronModel>();
            var r = new Random();
            for (var i = 0; i < lenght; i++)
            {

                Pool.Add(new NeuronModel { Value = r.NextDouble() });
            }
        }

        public TermModel Down(LanguageModel request)
        {
            var replacement = 1.0;
            var replacementindex = 0;
            request.MeaningValue = (Pool[0].Value + request.MeaningValue) / 2;
            if (request.MeaningValue < replacement)
            {
                replacement = request.MeaningValue;
                replacementindex = 0;
            }
            request.HistoryValue = (Pool[1].Value + request.HistoryValue) / 2;
            if (request.HistoryValue < replacement)
            {
                replacement = request.HistoryValue;
                replacementindex = 1;
            }
            var node = new TermModel()
            {
                MeaningValue = (Pool[2].Value + request.MeaningValue) / 2,
                TermValue = (Pool[3].Value + (request.HistoryValue + request.MeaningValue) / 2) / 2,
                HistoryValue = (Pool[4].Value + request.HistoryValue) / 2
            };
            if (node.MeaningValue < replacement)
            {
                replacement = node.MeaningValue;
                replacementindex = 2;
            }
            if (node.TermValue < replacement)
            {
                replacement = node.TermValue;
                replacementindex = 3;
            }
            if (node.HistoryValue < replacement)
            {
                replacement = node.HistoryValue;
                replacementindex = 4;
            }
            for (var i = 5; i + 2 < Pool.Count(); i += 3)
            {
                var dv = node.MeaningValue;
                var mv = node.TermValue;
                var hv = node.HistoryValue;
                node.MeaningValue = (Pool[i].Value + node.MeaningValue) / 2;
                node.TermValue = (Pool[i + 1].Value + node.TermValue) / 2;
                node.HistoryValue = (Pool[i + 2].Value + node.HistoryValue) / 2;
                node.MeaningValue = (node.MeaningValue + mv) / 2;
                node.TermValue = (node.TermValue + hv) / 2;
                node.HistoryValue = (node.HistoryValue + dv) / 2;
                if (node.MeaningValue < replacement)
                {
                    replacement = node.MeaningValue;
                    replacementindex = i;
                }
                if (node.TermValue < replacement)
                {
                    replacement = node.TermValue;
                    replacementindex = i + 1;
                }
                if (node.HistoryValue < replacement)
                {
                    replacement = node.HistoryValue;
                    replacementindex = i + 2;
                }

            }
            Pool[replacementindex].Value = replacement;
            return node;
        }

        public LanguageModel Up(TermModel request)
        {
            for (var i = Pool.Count() - 3; i > 1; i -= 3)
            {
                var dv = request.MeaningValue;
                var mv = request.TermValue;
                var hv = request.HistoryValue;
                request.MeaningValue = (Pool[i].Value + request.MeaningValue) / 2;
                request.TermValue = (Pool[i + 1].Value + request.TermValue) / 2;
                request.HistoryValue = (Pool[i + 2].Value + request.HistoryValue) / 2;
                request.MeaningValue = (request.MeaningValue + mv) / 2;
                request.TermValue = (request.TermValue + hv) / 2;
                request.HistoryValue = (request.HistoryValue + dv) / 2;
            }
            request.MeaningValue = (Pool[0].Value + (request.MeaningValue + request.TermValue) / 2) / 2;
            request.HistoryValue = (Pool[1].Value + (request.HistoryValue + request.TermValue) / 2) / 2;
            return request;
        }
    }
}