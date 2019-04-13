using KNCore.DAL.SYS;
using KNCore.IBLL.SYS;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.BLL.SYS
{
    public class SysDicBLL : CurdBLL<SysDic>, ISysDicBLL
    {
        public override void SetCurrentDal()
        {
            CurrentDAL = new SysDicDAL();
        }
    }

    public class AuthorityBLL : CurdBLL<Authority>, IAuthorityBLL
    {
        public override void SetCurrentDal()
        {
            CurrentDAL = new AuthorityDAL();
        }
    }
}
