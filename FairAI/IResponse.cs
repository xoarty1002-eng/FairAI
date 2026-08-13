using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface IResponse
    {
        public string Response {  get; set; }
        public void Generate(DataModel dm);
    }
}
