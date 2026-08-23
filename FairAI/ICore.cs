using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface ICore
    {
        public NodeModel Beam(NodeModel request);
        public void Drive();
    }
}
