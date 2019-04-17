using KNCore.BLL.SYS;
using KNCore.IBLL.SYS;
using KNCore.IService;
using KNCore.Model.SysModel;
using System;
using System.Collections.Generic;

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

        public bool UpdateAuth(Authority authority)
        {
            return _authorityBLL.Update(authority);
        }

        public bool delAuth(Authority authority)
        {
            return _authorityBLL.Del(authority);
        }

        public List<Authority> GetAllAuthorities()
        {
            
            return _authorityBLL.GetEntities(z => z.Id != 0);
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
