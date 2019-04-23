using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNCore.IBLL.SYS;
using KNCore.IService;
using KNCore.IService.ISysService;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetPostCount")]
        public string GetPostCount()
        {
            string count = "数量"+ _positionSer.CountPosition();
            return count;
        }

        [HttpPost("GetAuthCount")]
        public string GetAuthCount(int id)
        {
            var body = Request.Form.ToList();
            var query = Request.QueryString.ToString();
            return "数量:" + _sysAuthSer.CountAuth();
        }

        [HttpGet("Mytest")]
        public string Mytest()
        {
            return "mytest";
        }

    }
}