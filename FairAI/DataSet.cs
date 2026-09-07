using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public class DataSet
    {
        public List<CoreModel> Cores { get; set; }
        public List<NeuronModel> Pool { get; set; }
        public List<LanguageModel> Data = [];
        public void Load()
        {
            var ok = false;
        }
        public void Save()
        {
            var todo = true;
        }
    }
}
