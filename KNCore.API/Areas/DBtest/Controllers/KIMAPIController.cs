using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNCore.IBLL.Comm;
using KNCore.IBLL.SYS;
using KNCore.Model.SysModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KNCore.API.Areas.DBtest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KIMAPIController : ControllerBase
    {
        private readonly ILoginUserBLL _baseUserBLL;
        private readonly ISysDicBLL _sysDicBLL;


        public KIMAPIController(ILoginUserBLL baseUserBLL, ISysDicBLL sysDicBLL)
        {
            _baseUserBLL = baseUserBLL;
            _sysDicBLL = sysDicBLL;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get()
        {
            //所有post表单数据
            var post = HttpContext.Request.Form.Keys;
            //所有get参数
            var get = HttpContext.Request.QueryString;

            return "123";
        }

        [HttpGet("GetDicCount")]
        public string GetDicCount()
        {
            SysDic sysDic = new SysDic() { Diccode = "1", Dicname = "1", Dickey = "1", Dicvalue = "1", Dicmeno = "1", Dorder = "1" };
            int count = _sysDicBLL.Add(sysDic);
            return "ID:" + count;
        }

        [HttpDelete("deldic")]
        public string DelDic(string dickey)
        {
            var list = Request.Form.ToList();
            int count = _sysDicBLL.DelRange(e => e.Dickey == dickey);
            return "删除" + count;
        }

        [HttpPost("GetUserName")]
        public ActionResult<string> GetUserName(int id)
        {
            string username = _baseUserBLL.GetEntity(e => e.Id == id).UserName;
            return username;
        }

        [HttpPost]
        public ActionResult<string> Post([FromBody] int id)
        {
            string username = _baseUserBLL.GetEntity(e => e.Id == id).UserName;
            return username;
        }

    }
}