using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Application.Helpers.Extensions
{
    public static class ImageExtension
    {
        public static string Save(this IFormFile file)
        {
            

            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string directoryPath = Path.Combine("wwwroot", "img");

            // Ensure directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string path = Path.Combine(directoryPath, newFileName);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return newFileName;
        }
        public static void DeleteFile(this string fileName)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
