using KNCore.IDAL;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.DAL
{
    public class LogDAL<T> :MysqlLogDbContext<T>, ILogDAL<T>where T:class,new() 
    {
        public DbSet<T> CurrentDb { get { return new DbSet<T>(Db); } }

        public object DbContext { get => CurrentDb; }

        public int LogAdd(T obj)
        {
            return CurrentDb.AsInsertable(obj).ExecuteCommand();
        }
    }
}
