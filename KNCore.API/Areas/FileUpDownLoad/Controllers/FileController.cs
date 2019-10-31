using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        //无需定义file参数名只需要传文件
        public async Task<JsonResult> UpLoadFile(string id, IFormCollection file)
        {
            var filenames = file.Files;
            string fn = string.Empty;
            foreach (var filename in filenames)
            {
                var fileExt = FileHelper.GetFileExt(filename.FileName);
                var path = Utils.AssigendPath(fileExt, directoryRootName);
                FileHelperCore.CreateFiles(path);
                using (var stream = new FileStream(path + filename.FileName, FileMode.Create))
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

        [HttpGet("LoadDownFile")]
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

        //文件流下载
        [HttpGet("DownLoadFileInStream")]
        public IActionResult DownLoadFileInStream(string filePath)
        {
            //var memory = new MemoryStream();
            var fileName = Path.GetFileName(filePath);//测试文档.xlsx

            int bufferSize = 1;//这就是ASP.NET Core循环读取下载文件的缓存大小，这里我们设置为了1024字节，也就是说ASP.NET Core每次会从下载文件中读取1024字节的内容到服务器内存中，然后发送到客户端浏览器，这样避免了一次将整个下载文件都加载到服务器内存中，导致服务器崩溃

            Response.ContentType = "application/octet-stream";//由于我们下载的是一个Excel文件，所以设置ContentType为application/vnd.ms-excel

            var contentDisposition = "attachment;" + "filename=" + HttpUtility.UrlEncode(fileName);//在Response的Header中设置下载文件的文件名，这样客户端浏览器才能正确显示下载的文件名，注意这里要用HttpUtility.UrlEncode编码文件名，否则有些浏览器可能会显示乱码文件名
            Response.Headers.Add("Content-Disposition", new string[] { contentDisposition });

            //使用FileStream开始循环读取要下载文件的内容
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (Response.Body)//调用Response.Body.Dispose()并不会关闭客户端浏览器到ASP.NET Core服务器的连接，之后还可以继续往Response.Body中写入数据
                {
                    long contentLength = fs.Length;//获取下载文件的大小
                    Response.ContentLength = contentLength;//在Response的Header中设置下载文件的大小，这样客户端浏览器才能正确显示下载的进度

                    byte[] buffer;
                    long hasRead = 0;//变量hasRead用于记录已经发送了多少字节的数据到客户端浏览器

                    //如果hasRead小于contentLength，说明下载文件还没读取完毕，继续循环读取下载文件的内容，并发送到客户端浏览器
                    while (hasRead < contentLength)
                    {
                        //HttpContext.RequestAborted.IsCancellationRequested可用于检测客户端浏览器和ASP.NET Core服务器之间的连接状态，如果HttpContext.RequestAborted.IsCancellationRequested返回true，说明客户端浏览器中断了连接
                        if (HttpContext.RequestAborted.IsCancellationRequested)
                        {
                            //如果客户端浏览器中断了到ASP.NET Core服务器的连接，这里应该立刻break，取消下载文件的读取和发送，避免服务器耗费资源
                            break;
                        }

                        buffer = new byte[bufferSize];

                        int currentRead = fs.Read(buffer, 0, bufferSize);//从下载文件中读取bufferSize(1024字节)大小的内容到服务器内存中

                        Response.Body.Write(buffer, 0, currentRead);//发送读取的内容数据到客户端浏览器
                        Response.Body.Flush();//注意每次Write后，要及时调用Flush方法，及时释放服务器内存空间

                        hasRead += currentRead;//更新已经发送到客户端浏览器的字节数
                    }
                }
            }

            return new EmptyResult();
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
            return new Dictionary<string, string> { { ".txt", "text/plain" }, { ".pdf", "application/pdf" }, { ".doc", "application/vnd.ms-word" }, { ".docx", "application/vnd.ms-word" }, { ".xls", "application/vnd.ms-excel" }, { ".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet" }, { ".png", "image/png" }, { ".jpg", "image/jpeg" }, { ".jpeg", "image/jpeg" }, { ".gif", "image/gif" }, { ".csv", "text/csv" },{".exe", "application/octet-stream" } };
        }


    }
}