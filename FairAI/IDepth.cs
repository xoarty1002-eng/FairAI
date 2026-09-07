using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface IDepth
    {
        public List<NeuronModel> Pool { get; set; }
        public TermModel Down(LanguageModel request);
        public LanguageModel Up(TermModel request);
    }
}
