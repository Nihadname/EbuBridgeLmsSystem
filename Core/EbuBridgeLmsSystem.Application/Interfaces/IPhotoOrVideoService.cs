using EbuBridgeLmsSystem.Application.Helpers.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.AccessControl;

namespace EbuBridgeLmsSystem.Application.Interfaces
{
    public interface IPhotoOrVideoService
    {
        Task<string> UploadMediaAsync(IFormFile file, bool isVideo = false);
        Task<string> DeleteMediaAsync(string mediaUrl, FileResourceType resourceType);
    }
}
