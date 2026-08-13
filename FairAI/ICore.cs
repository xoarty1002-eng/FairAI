using System;
using System.Collections.Generic;
using System.Text;

namespace FairAI
{
    public interface ICore
    {
        public List<CoreModel> Cores { get; set; }
        public void Create(int count);
        public DataModel Drive(CoreModel X, CoreModel Y, CoreModel Z);
        public DataModel Update(CoreModel X, CoreModel Y, CoreModel Z);
    }
}
