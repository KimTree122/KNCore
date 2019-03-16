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
            MysqlBaseDbContext<BaseUser> sugarDb = new MysqlBaseDbContext<BaseUser>();
            sugarDb.Db.DbFirst.Where("sysdic").CreateClassFile(@"C:\DataBase\SqlClz");
            
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void CreatLog()
        {
            MysqlLogDbContext<BaseUser> logdb = new MysqlLogDbContext<BaseUser>();
            logdb.Db.DbFirst.Where("LogData").CreateClassFile(@"C:\DataBase\SqlClz");
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void DbUpdateRange()
        {
            BaseUserBLL bus = new BaseUserBLL();
            List<BaseUser> buls = bus.GetPageEntityes(bu => bu.Del == false, bu => bu.Id, 10, 1,true);
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
            BaseUserBLL bus = new BaseUserBLL();
            MD5Helper mD5Helper = new MD5Helper();
            BaseUser bu = new BaseUser() { UserName = "test", UserCode = "test", LogPWD = mD5Helper.CreateMD5Hash("test"), Del = false };
            int nbu = bus.Add(bu);
            
            Assert.AreEqual(2, nbu);
        }

        [TestMethod]
        public void DbUpdate()
        {
            BaseUserBLL bus = new BaseUserBLL();
            MD5Helper mD5Helper = new MD5Helper();
            BaseUser bu = new BaseUser() { Id = 2, UserName = "test", UserCode = "test", LogPWD = mD5Helper.CreateMD5Hash("test"), Del = true };

            int count = bus.Update(bu) ? 1 : 0;
            Assert.AreEqual(1, count);

        }

        [TestMethod]
        public void DbDelete()
        {
            BaseUserBLL bus = new BaseUserBLL();
            BaseUser bu = new BaseUser() { Id = 2 };
            int count = bus.Del(bu) ? 1 : 0;
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void DbSelect()
        {
            BaseUserBLL bus = new BaseUserBLL();
            List<BaseUser> users = bus.GetEntities(bs => bs.Del == false);
            int count = users.Count;
            Assert.AreEqual(2,count);
        }

    }
}
