using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public class CoreDepth : ICore
    {
        public List<CoreModel> Cores { get; set; }
        public CoreDepth(int count)
        {
            var r = new Random();
            for (var i = 0; i < count; i++) 
            {
                Cores.Add(new CoreModel() {Lenght = i, Position = 0, Speed = r.NextDouble(), Night=r.NextDouble(), Balance=0 });
            }
        }

        public NodeModel Beam(NodeModel request)
        {
            Cores.Remove(Cores.MinBy(a => a.Balance));
            Cores.Add(new CoreModel() { Lenght = request.DepthValue / Cores.Count , Position = 0, Speed = request.HistoryValue, Night = request.MiddleValue, Balance=0 });
            while (true) 
            {
                Drive();
                if (true)
                {
                    break;
                    //beam here
                }
            }
            return new NodeModel();
        }

        public void Drive()
        {
            foreach(var item in Cores)
            {
                item.Position += item.Speed;
            }
        }

    }
}
