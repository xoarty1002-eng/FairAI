using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface IDepth
    {
        public List<NeuronModel> Pool { get; set; }
        public void Generate(int lenght);
        public void Update(NeuronModel first, NeuronModel last);
    }
}
