using KNCore.BLL.SYS;
using KNCore.IBLL.SYS;
using KNCore.IService;
using KNCore.Model.SysModel;
using System;

namespace KNCore.Service
{
    public class AuthoritySer:IAuthoritySer
    {

        private readonly IAuthorityBLL _authorityBLL;

        public AuthoritySer()
        {
            _authorityBLL = new AuthorityBLL();
        }

        public int AddAuth(Authority authority)
        {
            int add = _authorityBLL.Add(authority);
            return add;
        }

        public int CountAuth()
        {
            int count = _authorityBLL.GetEntities(z => z.Id != 0).Count;
            return count;
        }

        public void Dispose()
        {
            
        }
    }
}
