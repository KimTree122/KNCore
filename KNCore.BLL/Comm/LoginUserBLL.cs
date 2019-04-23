using KNCore.DAL;
using KNCore.DAL.Comm;
using KNCore.IBLL.Comm;
using KNCore.Model.CommModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KNCore.BLL.Comm
{
    public class LoginUserBLL : CurdBLL<LoginUser>, ILoginUserBLL
    {
        public override void SetCurrentDal()
        {
            CurrentDAL = new LoginUserDAL();
            Dbset = CurrentDAL.DbContext as DbSet<LoginUser>;
        }

        public List<LoginUser> ForSQLT()
        {
            string sql = string.Format("SELECT * FROM dbo.LoginUser");
             var loguserlist = Dbset.FullClient.SqlQueryable<LoginUser>(sql).ToList();
            return loguserlist;
        }
    }
}
