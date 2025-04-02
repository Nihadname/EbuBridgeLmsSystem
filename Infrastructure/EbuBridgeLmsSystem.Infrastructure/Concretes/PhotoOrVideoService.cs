using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EbuBridgeLmsSystem.Application.Exceptions;
using EbuBridgeLmsSystem.Application.Helpers.Enums;
using EbuBridgeLmsSystem.Application.Interfaces;
using EbuBridgeLmsSystem.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace EbuBridgeLmsSystem.Infrastructure.Concretes
{
    public class PhotoOrVideoService : IPhotoOrVideoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoOrVideoService(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> DeleteMediaAsync(string mediaUrl, FileResourceType resourceType)
        {
            ResourceType resourceTypeMain = new ResourceType();
            resourceTypeMain = (ResourceType)resourceType;
            string publicId = await ExtractPublicIdFromUrl(mediaUrl);
            var deletionParams = new DeletionParams(publicId) { ResourceType = resourceTypeMain };
            var result = await _cloudinary.DestroyAsync(deletionParams);
            if (result.Result != "ok")
                throw new CustomException(500, "Image", "Image delete error");
            return "deleted";
        }
        public async Task<string> UploadMediaAsync(IFormFile file, bool isVideo = false,bool isExtraAsset=false)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            object uploadResult;
            if (isVideo)
            {
                uploadResult = new VideoUploadResult();
            }
            else
            {
                if (isExtraAsset)
                {
                    uploadResult = new RawUploadResult();
                }
                else
                {
                    uploadResult = new ImageUploadResult();
                }
            }

            using (var stream = file.OpenReadStream())
            {
                if (isVideo)
                {
                    var uploadParams = new VideoUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Quality("auto").FetchFormat("auto") // Adjust for videos
                    };


                    uploadResult = await _cloudinary.UploadAsync(uploadParams);


                }
                else
                {
                    if (isExtraAsset)
                    {
                        var uploadParams = new RawUploadParams()
                        {
                            File = new FileDescription(file.FileName, stream)

                        };

                    }
                    else
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(file.FileName, stream),
                            Transformation = new Transformation().Width(500).Height(500).Crop("fill").Quality("auto").FetchFormat("auto") // Adjust for videos
                        };


                        uploadResult = await _cloudinary.UploadAsync(uploadParams);
                    }


                }


            }
            if (isVideo)
            {
                var result = (VideoUploadResult)uploadResult;
                if (result.Error != null)
                {
                    throw new CustomException(400, result.Error.Message);
                }
                return result.SecureUrl.ToString();
            }
            else
            {
                var result = (ImageUploadResult)uploadResult;
                if (result.Error != null)
                {
                    throw new CustomException(400, result.Error.Message);
                }
                return result.SecureUrl.ToString();
            }
        }
        public  string UploadMediaAsyncWithUrl(string url)
        {
            if (!File.Exists(url))
            {
                throw new FileNotFoundException("Image not found at specified path.");
            }
            using (var fs = new FileStream(url, FileMode.Open))
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(Path.GetFileName(url), fs)
                };

                
                var uploadResult = _cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                   
                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    throw new Exception("Error uploading image to Cloudinary.");
                }
            }
        }
        private async Task<string> ExtractPublicIdFromUrl(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                string path = uri.AbsolutePath;
                string publicId = path.Split('/').Last().Split('.')[0];

                return publicId;
            }
            catch
            {
                throw new CustomException(400, "Invalid URL", "Could not extract public ID from URL");
            }

        }
    }
}
