﻿using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Route_C41_G02_PL.Helpers
{
    public static class DocumentSettings
    {
        public static async Task<string> UploadFile(IFormFile file, string folderName)
        {
            // 1- Get Located Folder Path
            //string folderPath = $"D:\\Route.Net Course\\08 Asp.Net Core MVC\\Session 03\\Assignements\\Route-C41-G02\\Route-C41-G02-PL\\wwwroot\\files\\{folderName}";

            //string folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{folderName}";

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            // 2- Get File Name and Make it Unique
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            // 3- Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            // 4- Save File as Streams[Data Per Time]
            using var fileStream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return fileName;


        }
        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
