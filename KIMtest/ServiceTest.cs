using KNCore.Comm.DataSwitch;
using KNCore.Comm.TreeNodeHelper;
using KNCore.IService;
using KNCore.IService.ISysService;
using KNCore.Model.SysModel;
using KNCore.Service;
using KNCore.Service.SysService;
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
        private readonly IPositionSer _positionSer;

        public ServiceTest()
        {
            _authoritySer = new AuthoritySer();
            _positionSer = new PositionSer();
        }

        [TestMethod]
        public void PositionInit()
        {
            List<Position> positions = _positionSer.GetAllPosition();
            TreeNodeComm nodeComm = new TreeNodeComm();
            var webnodes = nodeComm.InitTreeNode<Position>(positions,PositionDic,4);
            string str = JSonHelper.ObjectToJson(webnodes);
            Assert.AreEqual(11, positions.Count);
        }

        public Dictionary<string, string> PositionDic(object p)
        {
            return new Dictionary<string, string>();
        }

        [TestMethod]
        public void PositionAdd()
        {
            Position p = new Position() {
                FatherID= 2,
                NodeName = "Transfer"
            };
            int id = _positionSer.AddPosition(p);
            Assert.AreEqual(10,id);
            
        }


        [TestMethod]
        public void AuthorityInit()
        {
            TreeNodeComm treeNode = new TreeNodeComm();
            List<Authority> authorities = _authoritySer.GetAllAuthorities();

            List<WebTreeNode> webNodelist = treeNode
                .InitTreeNode(authorities, TreeAttr);

            string str = JSonHelper.ObjectToJson(webNodelist);

            Assert.AreEqual(authorities.Count, 9);
        }

        private Dictionary<string, string> TreeAttr(object auth)
        {
            Authority au = (Authority)auth;
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "url", au.Path },{ "AOrder",au.Order.ToString()}, {"AuthTypeID",au.AuthTypeID.ToString() },{ "ParentID",au.FatherID.ToString()}
            };
            return dic;
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
