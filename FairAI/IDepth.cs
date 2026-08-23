using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface IDepth
    {
        public List<NeuronModel> Pool { get; set; }
        public void Update(NeuronModel first, NeuronModel last);
        public NodeModel Down(StateModel request);
        public StateModel Up(NodeModel request);
    }
}
