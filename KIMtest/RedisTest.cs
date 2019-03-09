using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Weiz.Redis.RedisTest;

namespace KIMtest
{
    [TestClass]
    public class RedisTest
    {
        [TestMethod]
        public void redisread()
        {
            DateTime dtime = new DateTime(2019, 03, 10);
            RedisCacheHelper.Add<string>("kim", "test2", dtime);

            string str = RedisCacheHelper.Get<string>("kim");
            
            Assert.AreEqual("test2", str);
        }

        [TestMethod]
        public void RedisAdd()
        {
            DateTime dtime = new DateTime(2019, 03, 10);
            RedisCacheHelper.Add<string>("kim2", "test", dtime);
            string str = RedisCacheHelper.Get<string>("kim2");
            Assert.AreEqual("test",str);
        }

        [TestMethod]
        public void RedisRemove()
        {
            RedisCacheHelper.Remove("kim2");
            string str = RedisCacheHelper.Get<string>("kim2");
            Assert.AreEqual(null, str);
        }



    }
}
