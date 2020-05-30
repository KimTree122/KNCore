using KNCore.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.Service
{
    public class GenericeService<T> : IGenericeService<T>
    {
        public T Data { get; private set; }

        public GenericeService(T data)
        {
            this.Data = data;
        }
    }
}
