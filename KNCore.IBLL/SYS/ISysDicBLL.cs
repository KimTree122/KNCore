using KNCore.Comm.ServiceRegistry;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNCore.IBLL.SYS
{
    //字典
    public interface ISysDicBLL:ICurdBLL<SysDic>{ }

    //权限
    public interface IAuthorityBLL : ICurdBLL<Authority> { }
}
