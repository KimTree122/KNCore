using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Comm.Oberserver
{
    public class Subject
    {
        private List<Observer> observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Notice()
        {
            foreach (var ob in observers)
            {
                ob.ToDo();
            }
        }

    }
}
