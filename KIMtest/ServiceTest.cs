using KNCore.Comm.TreeNodeHelper;
using KNCore.IService;
using KNCore.Model.SysModel;
using KNCore.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KIMtest
{
    [TestClass]
    public class ServiceTest
    {
        private readonly IAuthoritySer _authoritySer;

        public ServiceTest()
        {
            _authoritySer = new AuthoritySer();
        }

        [TestMethod]
        public void TreeTest()
        {
            TreeNodeComm<Authority> treeNode = new TreeNodeComm<Authority>();
            List<Authority> authorities = _authoritySer.GetAllAuthorities();
            var tree = treeNode.InitTreeNode(authorities);
            List<string> strlist = treeNode.TranslateWebNode(authorities);
            Assert.AreEqual(authorities.Count, 9);
        }

        [TestMethod]
        public void AddAuthity()
        {
            Authority authority = new Authority()
            {
                FatherID = 5,
                NodeName = "MVC",
            };
            int id = _authoritySer.AddAuth(authority);
            Assert.AreEqual(id, 9);
        }

        [TestMethod]
        public void CountAuthity()
        {
            int count = _authoritySer.CountAuth();
            Assert.AreEqual(7, count);
        }


    }
}
