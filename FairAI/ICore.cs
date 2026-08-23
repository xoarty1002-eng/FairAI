using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface ICore
    {
        public List<CoreModel> Cores { get; set; }
        public StateModel Beam(StateModel request);
    }
}
