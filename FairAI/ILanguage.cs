using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface ILanguage
    {
        public void Add(string Word);
        public string Generate(StateModel dm);
        public StateModel Calculate(string request);
    }
}
