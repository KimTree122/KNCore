using SqlSugar;

namespace KNCore.DAL
{

    public class MysqlLogDbContext<T> where T : class, new()
    {
        public SqlSugarClient Db;

        public MysqlLogDbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                //ConnectionString = ConfigExtensions.Configuration["ConnectionStrings:MysqlConn"],
                ConnectionString = "server=192.168.99.100;database=logdb;uid=kim;pwd=147852;pooling=true;CharSet=utf8;port=3308;sslmode=none",
                DbType = DbType.MySql,
                InitKeyType = InitKeyType.SystemTable,
                IsAutoCloseConnection = true
            });

            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //sql log
            };
        }

        public DbSet<T> EntityDb { get { return new DbSet<T>(Db); } }

    }
}
