using KNCore.Comm;
using KNCore.Model.CommModel;
using SqlSugar;
using System;

namespace KNCore.DAL
{
    public class MysqlBaseDbContext<T>where T:class,new()
    {
        public SqlSugarClient Db;

        public MysqlBaseDbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig() {
                ConnectionString = ConfigExtensions.Configuration["ConnectionStrings:WriteDB01"],
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.Attribute,
                IsAutoCloseConnection = true,
                SlaveConnectionConfigs = new System.Collections.Generic.List<SlaveConnectionConfig>() {
                    new SlaveConnectionConfig(){ HitRate = 10, ConnectionString = ConfigExtensions.Configuration["ConnectionStrings:ReadDB01"] },
                    new SlaveConnectionConfig(){ HitRate = 10,ConnectionString = ConfigExtensions.Configuration["ConnectionStrings:ReadDB02"] }
                }
            });

            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //sql log
            };
        }

        
        //public DbSet<BaseUser> baseUserDB { get { return new DbSet<BaseUser>(Db); } }
        public DbSet<T> EntityDb { get { return new DbSet<T>(Db); } }

    }

    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        public DbSet(SqlSugarClient context) : base(context) { }
    }

}
