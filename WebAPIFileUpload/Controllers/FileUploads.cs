using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPIFileUpload.Models;

namespace WebAPIFileUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploads : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;

        public FileUploads(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public string Post([FromForm] FileUpload objectFile)
        {
            try
            {
                if (objectFile.files.Length > 0)
                {
                    string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using(FileStream fileStream = System.IO.File.Create(path + objectFile.files.FileName ))
                    {
                        objectFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "Uploaded.";
                    }
                }
                else
                {
                    return "Not Uploaded.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
