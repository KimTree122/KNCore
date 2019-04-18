using KNCore.Comm.ServiceRegistry;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;

namespace KNCore.IService
{
    public interface IAuthoritySer:IAppService
    {
        int AddAuth(Authority authority);
        int CountAuth();
        List<Authority> GetAllAuthorities();
        bool UpdateAuth(Authority authority);
        bool DelAuth(Authority authority);

    }
}
