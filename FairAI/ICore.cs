using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface ICore
    {
        public TermModel Check(TermModel request);
        public void Drive(int time);
    }
}
