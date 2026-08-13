using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface IRequest
    {
        public DataModel DM {  get; set; }
        public void Read(string Request);
    }
}
