﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.WebUI.Infrastructure.Helpers
{
    public class Upload
    {
        public static string ImageUpload(List<IFormFile> files, IWebHostEnvironment _env, out bool result)
        {
            result = false;
            //Path.Combine iki path birleştirme
            var uploads = Path.Combine(_env.WebRootPath,"uploads");
            foreach (var file in files)
            {
                if (file.ContentType.Contains("image"))
                {
                    if (file.Length <= 3000000)
                    {
                        string uniqueName = 
                            $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split("/")[1]}";
                        var filePath = Path.Combine(uploads, uniqueName);
                        using (var fileStream = new FileStream(filePath,FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            result = true;
                            return filePath.Substring(filePath.IndexOf("\\uploads\\"));
                        }
                    }
                    else
                    {
                        return "3MB'dan büyük boyutta resim yükleyemezsiniz.";
                    }
                }
                else
                {
                    return "Lütfen sadece resim dosyası seçiniz.";
                }
            }
            return "Dosya Bulunamadı! Lütfen en az bir dosya seçiniz...";
        }
    }
}
