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
        public void stam()
        {
            Assert.AreEqual("sadmin", "");
        }


        [TestMethod]
        public async Task DapperGetAsync()
        {
            LoginUser bu = await DapperDataAsync.GetAsync<LoginUser>(1, null, null);
            Assert.AreEqual("sadmin", bu.UserName);
        }

        [TestMethod]
        public async Task DapperInsert()
        {
            LoginUser bu = await DapperDataAsync.GetAsync<LoginUser>(2, null, null);
            bu.UserCode = "testNo2";
            bu.UserName = "testerNo2";

            int id = await DapperDataAsync.InsertAsync<LoginUser>(bu, null, null);
            Assert.AreEqual(6, id);
        }

        [TestMethod]
        public async Task DapperDel()
        {
            LoginUser bu = new LoginUser() { Id = 6 };
            bool b = await DapperDataAsync.DeleteAsync<LoginUser>(bu);

            Assert.AreEqual(true, b);
        }

        public void test()
        {
            LoginUser bu = new LoginUser() { Id = 6 };
            bool b = true;
            Task.Run(async () => {
               b =  await DapperDataAsync.DeleteAsync<LoginUser>(bu);
            });
        }

        [TestMethod]
        public async Task DapperUpdateAsync()
        {
            LoginUser bu = await DapperDataAsync.GetAsync<LoginUser>(2, null, null);
            bu.UserName = "tester";
            bu.Del = false;
            LoginUser newbu = await DapperDataAsync.UpdateAsync<LoginUser>(bu, null, null);
            Assert.AreEqual("tester",newbu.UserName);
        }
    }
}
