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
        private readonly IBaseUserBLL _baseUserBLL;
        private readonly ISysDicBLL _sysDicBLL;

        public KIMAPIController(IBaseUserBLL baseUserBLL,ISysDicBLL sysDicBLL)
        {
            _baseUserBLL = baseUserBLL;
            _sysDicBLL = sysDicBLL;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get()
        {
            return "123";
        }

        [HttpGet("GetDicCount")]
        public string GetDicCount()
        {
            SysDic sysDic = new SysDic() { diccode = "1", dicname = "1", dickey = "1",dicvalue = "1", dicmeno = "1", order= 1 };
            int count = _sysDicBLL.Add(sysDic);
            return "ID:"+count;
        }

        [HttpGet("deldic")]
        public string DelDic()
        {
            int count = _sysDicBLL.DelRange(e => e.dickey == "1");
            return "删除"+count;
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