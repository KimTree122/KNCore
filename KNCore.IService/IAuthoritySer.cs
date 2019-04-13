using KNCore.Comm.ServiceRegistry;
using KNCore.Model.SysModel;
using System;

namespace KNCore.IService
{
    public interface IAuthoritySer:IAppService
    {
        int AddAuth(Authority authority);
        int CountAuth();
    }
}
