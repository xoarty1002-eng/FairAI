using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public class NeuronModel
    {
        public double Value {  get; set; }
        public virtual NeuronModel? RightParent { get; set; }
        public virtual NeuronModel? RightChild { get; set; }
        public virtual NeuronModel? LeftParent { get; set; }
        public virtual NeuronModel? LeftChild { get; set; }
        public double? Pocket { get; set; }

    }
}
