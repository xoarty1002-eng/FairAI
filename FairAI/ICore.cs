using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface ICore
    {
        public NodeModel Check(NodeModel request);
        public void Drive(int time);
    }
}
