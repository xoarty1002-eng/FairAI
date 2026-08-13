using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public class DataModel
    {
        public required string Word {  get; set; }
        public required double DepthValue {  get; set; }
        public required double HistoryValue {  get; set; }
    }
}
