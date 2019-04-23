using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNCore.BLL.Comm;
using KNCore.IBLL.Comm;
using Microsoft.AspNetCore.Mvc;

namespace KNCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ILoginUserBLL _baseUserBLL;

        public ValuesController(ILoginUserBLL userBLL)
        {
            _baseUserBLL = userBLL;
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody] string value)
        {
            return "post"+ value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("test")]
        public string Usertest(int id)
        {
            var Entitys = _baseUserBLL.GetEntity(e => e.Id == id);
            return "数量为:"+ Entitys.UserName;
        }
    }
}
