using EbuBridgeLmsSystem.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace EbuBridgeLmsSystem.Application.Helpers.Extensions
{
    public static class ImageExtension
    {
        public static string Save(this IFormFile file,Guid? courseId)
        {
            bool isValidGuid = courseId.HasValue && courseId != Guid.Empty;
            if (!isValidGuid)
            {
                throw new CustomException(400, "course is invalid");
            }

            string newFileName = courseId + Path.GetExtension(file.FileName);
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
        public static string GetImageFileNameFromCourseId(Guid courseId)
        {
            string directoryPath = Path.Combine("wwwroot", "img"); 
            string courseIdFileName = courseId.ToString() + Path.GetExtension("image.png");

            string filePath = Path.Combine(directoryPath, courseIdFileName);

            
            if (File.Exists(filePath))
            {
                return filePath; 
            }
            else
            {
                throw new CustomException(404, "Image for the given courseId not found.");
            }
        }

    }
}
