using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Comm.Oberserver
{
    public class ConcreteObserver:Observer
    {
        private string _subName;

        public ConcreteObserver(string subName)
        {
            _subName = subName;
        }

        public override void ToDo()
        {
            //todo
        }
    }
}
