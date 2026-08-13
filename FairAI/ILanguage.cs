using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface ILanguage
    {
        public List<DataModel> Data { get; set; }
        public void Add(string Word);
        public string Generate(DataModel dm);
        public string Calculate(string request);
    }
}
