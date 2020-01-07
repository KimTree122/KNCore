using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Comm.Oberserver
{
    public class ObserverTest
    {
        public void DoObserver()
        {
            ConcreteSubject concrete = new ConcreteSubject();
            Observer ob_a = new ConcreteObserver("A");
            Observer ob_b = new ConcreteObserver("B");
            concrete.Attach(ob_a);
            concrete.Attach(ob_b);
            concrete.Notice();

        }
    }
}
