using KNCore.DapperDAL;
using KNCore.Model.CommModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KIMtest
{
    [TestClass]
    public class DapperTest
    {
        [TestMethod]
        public async Task DapperGetAsync()
        {
            BaseUser bu = await DapperDataAsync.GetAsync<BaseUser>(1, null, null);
            Assert.AreEqual("sadmin", bu.UserName);
        }

        [TestMethod]
        public async Task DapperInsert()
        {
            BaseUser bu = await DapperDataAsync.GetAsync<BaseUser>(2, null, null);
            bu.UserCode = "testNo2";
            bu.UserName = "testerNo2";
            
            int id =await DapperDataAsync.InsertAsync<BaseUser>(bu, null, null);
            Assert.AreEqual(6,id);
        }

        [TestMethod]
        public async Task DapperDel()
        {
            BaseUser bu = new BaseUser() { Id = 6 };
            bool b = await DapperDataAsync.DeleteAsync<BaseUser>(bu);
            Assert.AreEqual(true,b);
        }

        [TestMethod]
        public async Task DapperUpdateAsync()
        {
            BaseUser bu = await DapperDataAsync.GetAsync<BaseUser>(2, null, null);
            bu.UserName = "tester";
            bu.Del = false;
            BaseUser newbu = await DapperDataAsync.UpdateAsync<BaseUser>(bu, null, null);
            Assert.AreEqual("tester",newbu.UserName);
        }
    }
}
