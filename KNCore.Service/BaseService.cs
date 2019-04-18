using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Service
{
    public class BaseService
    {
        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }
    }
}
