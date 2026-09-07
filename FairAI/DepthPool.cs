using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace FairAI
{
    public class DepthPool : IDepth
    {
        public List<NeuronModel> Pool { get; set; }

        public DepthPool(int lenght)
        {
            Pool = new List<NeuronModel>();
            var r = new Random();
            for (var i = 0; i < lenght; i++)
            {

                Pool.Add(new NeuronModel { Value = r.NextDouble() });
            }
        }

        public NodeModel Down(StateModel request)
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
            var node = new NodeModel()
            {
                MeaningValue = (Pool[2].Value + request.MeaningValue) / 2,
                TimeValue = (Pool[3].Value + (request.HistoryValue + request.MeaningValue) / 2) / 2,
                HistoryValue = (Pool[4].Value + request.HistoryValue) / 2
            };
            if (node.MeaningValue < replacement)
            {
                replacement = node.MeaningValue;
                replacementindex = 2;
            }
            if (node.TimeValue < replacement)
            {
                replacement = node.TimeValue;
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
                var mv = node.TimeValue;
                var hv = node.HistoryValue;
                node.MeaningValue = (Pool[i].Value + node.MeaningValue) / 2;
                node.TimeValue = (Pool[i + 1].Value + node.TimeValue) / 2;
                node.HistoryValue = (Pool[i + 2].Value + node.HistoryValue) / 2;
                node.MeaningValue = (node.MeaningValue + mv) / 2;
                node.TimeValue = (node.TimeValue + hv) / 2;
                node.HistoryValue = (node.HistoryValue + dv) / 2;
                if (node.MeaningValue < replacement)
                {
                    replacement = node.MeaningValue;
                    replacementindex = i;
                }
                if (node.TimeValue < replacement)
                {
                    replacement = node.TimeValue;
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

        public StateModel Up(NodeModel request)
        {
            for (var i = Pool.Count() - 3; i > 1; i -= 3)
            {
                var dv = request.MeaningValue;
                var mv = request.TimeValue;
                var hv = request.HistoryValue;
                request.MeaningValue = (Pool[i].Value + request.MeaningValue) / 2;
                request.TimeValue = (Pool[i + 1].Value + request.TimeValue) / 2;
                request.HistoryValue = (Pool[i + 2].Value + request.HistoryValue) / 2;
                request.MeaningValue = (request.MeaningValue + mv) / 2;
                request.TimeValue = (request.TimeValue + hv) / 2;
                request.HistoryValue = (request.HistoryValue + dv) / 2;
            }
            request.MeaningValue = (Pool[0].Value + (request.MeaningValue + request.TimeValue) / 2) / 2;
            request.HistoryValue = (Pool[1].Value + (request.HistoryValue + request.TimeValue) / 2) / 2;
            return request;
        }
    }
}