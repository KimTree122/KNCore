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
        private readonly string rootdic = ConfigExtensions.Configuration["UploadFile:DirectoryRootPath"];
        private readonly string directoryRootName = ConfigExtensions.Configuration["UploadFile:DirectoryName"];

        [HttpPost("UpLoadFile")]
        public async Task<JsonResult> UpLoadFile(IFormCollection file)
        {
            var filenames = file.Files;
            string fn = string.Empty;
            foreach (var filename in filenames)
            {
                var fileExt = FileHelper.GetFileExt(filename.FileName);
                var path = Utils.AssigendPath(fileExt, directoryRootName);
                FileHelperCore.CreateFiles(path);
                using (var stream = new FileStream(rootdic + "\\" + path + filename.FileName, FileMode.Create))
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
            return Json(fn);
        }

        [HttpPost("LoadDownFile")]
        public async Task<IActionResult> LoadDownFile(string filePath)
        {
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            var file = File(memory, GetContentType(filePath), Path.GetFileName(filePath));

            return file;
        }

        public async Task<IActionResult> Findimage(int width, string name)
        {

            var appPath = AppContext.BaseDirectory.Split("\\bin\\")[0] + "/image/";
            var errorImage = appPath + "404.png";//没有找到图片
            var imgPath = string.IsNullOrEmpty(name) ? errorImage : appPath + name;
            //获取图片的返回类型
            var contentTypDict = new Dictionary<string, string> {
                {"jpg","image/jpeg"},
                {"jpeg","image/jpeg"},
                {"jpe","image/jpeg"},
                {"png","image/png"},
                {"gif","image/gif"},
                {"ico","image/x-ico"},
                {"tif","image/tiff"},
                {"tiff","image/tiff"},
                {"fax","image/fax"},
                {"wbmp","image//vnd.wap.wbmp"},
                {"rp","image/vnd.rn-realpix"}
            };
            var contentTypeStr = "image/jpeg";
            var imgTypeSplit = name.Split('.');
            var imgType = imgTypeSplit[imgTypeSplit.Length - 1].ToLower();
            //未知的图片类型
            if (!contentTypDict.ContainsKey(imgType))
            {
                imgPath = errorImage;
            }
            else
            {
                contentTypeStr = contentTypDict[imgType];
            }
            var imagestrem = await FileHelperCore.GetImageAsync(width, imgPath);
            return new FileStreamResult(imagestrem, contentTypeStr);


            ////图片不存在
            //if (!new FileInfo(imgPath).Exists)
            //{
            //    imgPath = errorImage;
            //}
            ////原图
            //if (width <= 0)
            //{
            //    using (var sw = new FileStream(imgPath, FileMode.Open))
            //    {
            //        var bytes = new byte[sw.Length];
            //        sw.Read(bytes, 0, bytes.Length);
            //        sw.Close();
            //        return new FileContentResult(bytes, contentTypeStr);
            //    }
            //}
            ////缩小图片
            //using (var imgBmp = new Bitmap(imgPath))
            //{
            //    //找到新尺寸
            //    var oWidth = imgBmp.Width;
            //    var oHeight = imgBmp.Height;
            //    var height = oHeight;
            //    if (width > oWidth)
            //    {
            //        width = oWidth;
            //    }
            //    else
            //    {
            //        height = width * oHeight / oWidth;
            //    }
            //    var newImg = new Bitmap(imgBmp, width, height);
            //    newImg.SetResolution(72, 72);
            //    var ms = new MemoryStream();
            //    newImg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            //    var bytes = ms.GetBuffer();
            //    ms.Close();
            //    return new FileContentResult(bytes, contentTypeStr);
            //}
        }

        private string GetContentType(string path)
        {

            var types = GetMimeTypes();

            var ext = Path.GetExtension(path).ToLowerInvariant();

            return types[ext];

        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>{{".txt", "text/plain"},{".pdf", "application/pdf"},{".doc", "application/vnd.ms-word"},{".docx", "application/vnd.ms-word"},{".xls", "application/vnd.ms-excel"},{".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},{".png", "image/png"},{".jpg", "image/jpeg"},{".jpeg", "image/jpeg"},{".gif", "image/gif"},{".csv", "text/csv"}};
        }


    }
}