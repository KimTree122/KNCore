using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNCore.IBLL.SYS;
using Microsoft.AspNetCore.Mvc;

namespace KNCore.API.Areas.DBtest.Controllers
{
    [Route("DBtest/[controller]")]
    public class KIMTestController : Controller
    {
        private readonly ISysDicBLL _sysDicBLL;

        public KIMTestController(ISysDicBLL sysDicBLL)
        {
            _sysDicBLL = sysDicBLL;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetSysCount")]
        public string GetSysCount()
        {
            var count = _sysDicBLL.GetEntities(i => 1==1).Count();
            return "数量"+count;
        }
    }
}