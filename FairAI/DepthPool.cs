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
            request.DepthValue = (Pool[0].Value + request.DepthValue) / 2;
            if (request.DepthValue < replacement)
            {
                replacement = request.DepthValue;
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
                DepthValue = (Pool[2].Value + request.DepthValue) / 2,
                MiddleValue = (Pool[3].Value + (request.HistoryValue + request.DepthValue) / 2) / 2,
                HistoryValue = (Pool[4].Value + request.HistoryValue) / 2
            };
            if (node.DepthValue < replacement)
            {
                replacement = node.DepthValue;
                replacementindex = 2;
            }
            if (node.MiddleValue < replacement)
            {
                replacement = node.MiddleValue;
                replacementindex = 3;
            }
            if (node.HistoryValue < replacement)
            {
                replacement = node.HistoryValue;
                replacementindex = 4;
            }
            for (var i = 5; i + 2 < Pool.Count(); i += 3)
            {
                var dv = node.DepthValue;
                var mv = node.MiddleValue;
                var hv = node.HistoryValue;
                node.DepthValue = (Pool[i].Value + node.DepthValue) / 2;
                node.MiddleValue = (Pool[i + 1].Value + node.MiddleValue) / 2;
                node.HistoryValue = (Pool[i + 2].Value + node.HistoryValue) / 2;
                node.DepthValue = (node.DepthValue + mv) / 2;
                node.MiddleValue = (node.MiddleValue + hv) / 2;
                node.HistoryValue = (node.HistoryValue + dv) / 2;
                if (node.DepthValue < replacement)
                {
                    replacement = node.DepthValue;
                    replacementindex = i;
                }
                if (node.MiddleValue < replacement)
                {
                    replacement = node.MiddleValue;
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
                var dv = request.DepthValue;
                var mv = request.MiddleValue;
                var hv = request.HistoryValue;
                request.DepthValue = (Pool[i].Value + request.DepthValue) / 2;
                request.MiddleValue = (Pool[i + 1].Value + request.MiddleValue) / 2;
                request.HistoryValue = (Pool[i + 2].Value + request.HistoryValue) / 2;
                request.DepthValue = (request.DepthValue + mv) / 2;
                request.MiddleValue = (request.MiddleValue + hv) / 2;
                request.HistoryValue = (request.HistoryValue + dv) / 2;
            }
            request.DepthValue = (Pool[0].Value + (request.DepthValue + request.MiddleValue) / 2) / 2;
            request.HistoryValue = (Pool[1].Value + (request.HistoryValue + request.MiddleValue) / 2) / 2;
            return request;
        }
    }
}