using Microsoft.AspNetCore.Http;

namespace MotoDev.Application.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile formFile);

        IAsyncEnumerable<string> UploadImagesAsync(IEnumerable<IFormFile> formFiles);

        Task<bool> DeleteImageAsync(string url);
    }
}