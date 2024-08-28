using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using MotoDev.Application.Interfaces;

namespace MotoDev.Application.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<string> UploadImageAsync(IFormFile formFile)
        {
            if (formFile == null)
                return null;

            using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);

            var imageArray = stream.ToArray();
            using var imageStream = new MemoryStream(imageArray);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(formFile.Name, imageStream),
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.PublicId;
        }

        public async IAsyncEnumerable<string> UploadImagesAsync(IEnumerable<IFormFile> formFiles)
        {
            if (formFiles == null || formFiles.Count() < 1)
                yield return null;

            foreach (var file in formFiles)
                yield return await UploadImageAsync(file);
        }

        public async Task<bool> DeleteImageAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            try
            {
                await _cloudinary.DestroyAsync(new DeletionParams(url));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}