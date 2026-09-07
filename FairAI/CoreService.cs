using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public class CoreService : ICore
    {
        public List<CoreModel> Cores { get; set; }

        public CoreService(int count)
        {
            Cores = new List<CoreModel>();
            var r = new Random();
            for (var i = 0; i < count; i++)
            {
                Cores.Add(new CoreModel() { RangeValue = i, SpeedValue = r.NextDouble(), PositionValue = r.NextDouble() });
            }
        }
        public TermModel Check(TermModel request)
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
                            double pos1 = Cores[i].PositionValue;
                            double pos2 = Cores[j].PositionValue;
                            double pos3 = Cores[k].PositionValue;
                            double axis1 = pos1 >= 0.5 ? pos1 - 0.5 : pos1;
                            double axis2 = pos2 >= 0.5 ? pos2 - 0.5 : pos2;
                            double axis3 = pos3 >= 0.5 ? pos3 - 0.5 : pos3;
                            double d12 = Math.Min(Math.Abs(axis1 - axis2), 0.5 - Math.Abs(axis1 - axis2));
                            double d23 = Math.Min(Math.Abs(axis2 - axis3), 0.5 - Math.Abs(axis2 - axis3));
                            double d13 = Math.Min(Math.Abs(axis1 - axis3), 0.5 - Math.Abs(axis1 - axis3));
                            if (d12 <= normalizedTolerance && d23 <= normalizedTolerance && d13 <= normalizedTolerance)
                            {
                                request.MeaningValue = Cores[i].SpeedValue;
                                request.HistoryValue = Cores[j].SpeedValue;
                                request.TermValue = Cores[k].SpeedValue;
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
                core.PositionValue = (core.SpeedValue * time) % 1.0;
            }
        }
    }
}