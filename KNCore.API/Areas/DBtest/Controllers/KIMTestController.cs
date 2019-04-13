using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNCore.IBLL.SYS;
using KNCore.IService;
using Microsoft.AspNetCore.Mvc;

namespace KNCore.API.Areas.DBtest.Controllers
{
    [Route("DBtest/[controller]")]
    public class KIMTestController : Controller
    {
        private readonly IAuthoritySer _sysAuthBLL;

        public KIMTestController(IAuthoritySer sysAuthBLL)
        {
            _sysAuthBLL = sysAuthBLL;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("GetAuthCount")]
        public string GetAuthCount(int id)
        {
            var body = Request.Form.ToList();
            var query = Request.QueryString.ToString();
            return "数量:" + _sysAuthBLL.CountAuth();
        }

        [HttpGet("Mytest")]
        public string Mytest()
        {
            return "mytest";
        }

    }
}