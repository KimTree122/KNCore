using KNCore.IDAL.SYS;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.DAL.SYS
{
    //字典
    public class SysDicDAL:CurdDAL<SysDic>,ISysDicDAL{}

    //权限
    public class AuthorityDAL : CurdDAL<Authority>, ISysAuthDAL { }
}
