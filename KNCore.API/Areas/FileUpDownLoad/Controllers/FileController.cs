using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KNCore.Comm;
using KNCore.Comm.FileHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KNCore.API.Areas.FileUpDownLoad.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json", "multipart/form-data")]//此处为新增
    [Route("FileUpDownLoad/[controller]")]
    public class FileController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> Index(IFormCollection file)
        {
            var filenames = file.Files;
            string fn = string.Empty;
            string rootdic = ConfigExtensions.Configuration["UploadFileDirectory:FileDirectory"];

            foreach (var filename in filenames)
            {
                var fileExt = FileHelper.GetFileExt(filename.FileName);
                var path =  Utils.AssigendPath(fileExt, "wwwroot");
                FileHelperCore.CreateFiles(path);
                using (var stream = new FileStream(rootdic+"\\"+path + filename.FileName,FileMode.Create))
                {
                    try
                    {
                        await filename.CopyToAsync(stream);
                        await stream.FlushAsync();
                    }
                    catch (Exception ex)
                    {
                        return Json(ex.Message);
                    }
                   
                }
                fn += filename.FileName;
            }

            return  Json(fn);
        }
    }
}