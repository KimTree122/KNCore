using KNCore.DAL;
using KNCore.IBLL;
using KNCore.IDAL;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.BLL
{
    public class LogBLL : ILogBLL<Logdata>
    {
        public ILogDAL<Logdata> LogDal { get; set; }

        public LogBLL()
        {
            LogDal = new LogDAL<Logdata>(); 
        }

        public int LogAdd(Logdata obj)
        {
           return  LogDal.LogAdd(obj);
        }
    }
}
