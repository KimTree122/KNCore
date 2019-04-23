using KNCore.BLL.Comm;
using KNCore.Comm;
using KNCore.Comm.DataSwitch;
using KNCore.DAL;
using KNCore.Model.CommModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KIMtest
{
    [TestClass]
    public class DBtest
    {

        [TestMethod]
        public void CreatEntityCls()
        {
            MysqlBaseDbContext<LoginUser> sugarDb = new MysqlBaseDbContext<LoginUser>();
            sugarDb.Db.DbFirst.Where("SYS_LoginUser").CreateClassFile(@"C:\DataBase\SqlClz");
            
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void CreatLog()
        {
            MysqlLogDbContext<LoginUser> logdb = new MysqlLogDbContext<LoginUser>();
            logdb.Db.DbFirst.Where("LogData").CreateClassFile(@"C:\DataBase\SqlClz");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void DbUpdateRange()
        {
            LoginUserBLL bus = new LoginUserBLL();
            List<LoginUser> buls = bus.GetPageEntityes(bu => bu.Del == false, bu => bu.Id, 10, 1,true);
            buls[0].UserCode = "sadmin";
            buls[0].UserName = "sadmin";

            buls[1].UserCode = "systest";
            buls[1].UserName = "systest";

            bus.UpdateRange(buls,e=> e.UserCode.Length > 0 );

            Assert.AreEqual(2,buls.Count);
        }

        [TestMethod]
        public void DbInsert()
        {
            LoginUserBLL bus = new LoginUserBLL();
            MD5Helper mD5Helper = new MD5Helper();
            LoginUser bu = new LoginUser() { UserName = "test", UserCode = "test", LogPWD = mD5Helper.CreateMD5Hash("test"), Del = false };
            int nbu = bus.Add(bu);
            
            Assert.AreEqual(2, nbu);
        }

        [TestMethod]
        public void DbUpdate()
        {
            LoginUserBLL bus = new LoginUserBLL();
            MD5Helper mD5Helper = new MD5Helper();
            LoginUser bu = new LoginUser() { Id = 1, UserName = "sad", UserCode = "sad", LogPWD = mD5Helper.CreateMD5Hash("sad"), Del = false };

            int count = bus.Update(bu) ? 1 : 0;
            Assert.AreEqual(1, count);

        }

        [TestMethod]
        public void DbDelete()
        {
            LoginUserBLL bus = new LoginUserBLL();
            LoginUser bu = new LoginUser() { Id = 2 };
            int count = bus.Del(bu) ? 1 : 0;
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void DbSelect()
        {
            LoginUserBLL bus = new LoginUserBLL();
            List<LoginUser> users = bus.GetEntities(bs => bs.Del == false);
            int count = users.Count;
            Assert.AreEqual(2,count);
        }

    }
}
