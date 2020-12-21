using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KNCore.IBLL.SYS;
using KNCore.IService;
using KNCore.IService.ISysService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace KNCore.API.Areas.DBtest.Controllers
{
    [Route("DBtest/[controller]")]
    public class KIMTestController : Controller
    {
        private readonly IAuthoritySer _sysAuthSer;
        private readonly IPositionSer _positionSer;

        public KIMTestController(IAuthoritySer sysAuthSer,IPositionSer positionSer)
        {
            _sysAuthSer = sysAuthSer;
            _positionSer = positionSer;
        }
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 测试数量
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPostCount")]
        public string GetPostCount()
        {
            string count = "数量"+ _positionSer.CountPosition();
            return count;
        }

        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpPost("GetAuthCount")]
        public string GetAuthCount(int id)
        {
            var body = Request.Form.ToList();
            var query = Request.QueryString.ToString();
            return "数量:" + _sysAuthSer.CountAuth();
        }

        /// <summary>
        /// 这是一个带参数的get请求
        /// </summary>
        /// <remarks>
        /// 例子:
        /// Get api/Values/1
        /// </remarks>
        /// <param name="id">主键</param>
        /// <returns>测试字符串</returns> 
        /// <response code="201">返回value字符串</response>
        /// <response code="400">如果id为空</response>  
        // GET api/values/2
        [HttpGet("Mytest")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public string Mytest()
        {
            string name = string.Empty, value = string.Empty;
            string strjson = Request.Query["info"];
            JObject json = JObject.Parse(strjson);
            foreach (var pro in json.Properties())
            {
                name += pro.Name + ",";
                value += "'" + pro.Value + "',";
            }

            string str = "insert into xxx (" + name.Substring(0, name.Length - 1) + ") values (" + value.Substring(0, value.Length - 1) + ")";
            return str;
        }

        [HttpPost("InputStream")]
        public  string InputStream()
        {
            var buffer = new byte[Convert.ToInt32(Request.ContentLength)];
            //await Request.Body.ReadAsync(buffer, 0, buffer.Length);
            Request.Body.Read(buffer, 0, buffer.Length);
            var body = Encoding.UTF8.GetString(buffer);
            //string getjson = Encoding.UTF8.GetString(b);
            return body;
        }

    }
}