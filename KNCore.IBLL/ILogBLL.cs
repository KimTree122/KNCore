using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.IBLL
{
    public interface ILogBLL<T> where T:class,new()
    {
        int LogAdd(T obj);
    }
}
