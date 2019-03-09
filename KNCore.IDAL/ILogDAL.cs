using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.IDAL
{
    public interface ILogDAL<T> where T:class,new()
    {
        int LogAdd(T obj);
    }
}
