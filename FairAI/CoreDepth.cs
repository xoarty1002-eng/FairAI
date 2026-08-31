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
            Cores = new List<CoreModel>();
            var r = new Random();
            for (var i = 0; i < count; i++)
            {
                Cores.Add(new CoreModel() { Range = i, Speed = r.NextDouble(), Position = r.NextDouble() });
            }
        }
        public NodeModel Check(NodeModel request)
        {
            double normalizedTolerance = 0.0028;
            var time = 1;
            while (true)
            {
                Drive(time);
                for (int i = 0; i < Cores.Count; i++)
                {
                    for (int j = i + 1; j < Cores.Count; j++)
                    {
                        for (int k = j + 1; k < Cores.Count; k++)
                        {
                            double pos1 = Cores[i].Position;
                            double pos2 = Cores[j].Position;
                            double pos3 = Cores[k].Position;
                            double axis1 = pos1 >= 0.5 ? pos1 - 0.5 : pos1;
                            double axis2 = pos2 >= 0.5 ? pos2 - 0.5 : pos2;
                            double axis3 = pos3 >= 0.5 ? pos3 - 0.5 : pos3;
                            double d12 = Math.Min(Math.Abs(axis1 - axis2), 0.5 - Math.Abs(axis1 - axis2));
                            double d23 = Math.Min(Math.Abs(axis2 - axis3), 0.5 - Math.Abs(axis2 - axis3));
                            double d13 = Math.Min(Math.Abs(axis1 - axis3), 0.5 - Math.Abs(axis1 - axis3));
                            if (d12 <= normalizedTolerance && d23 <= normalizedTolerance && d13 <= normalizedTolerance)
                            {
                                request.DepthValue = Cores[i].Speed;
                                request.HistoryValue = Cores[j].Speed;
                                request.MiddleValue = Cores[k].Speed;
                                return request;
                            }
                        }
                    }
                }
                time++;
            }
        }

        public void Drive(int time)
        {
            foreach (var core in Cores)
            {
                core.Position = (core.Speed * time) % 1.0;
            }
        }
    }
}