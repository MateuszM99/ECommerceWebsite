using ECommerceModels.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface IUploadServices
    {
        Task UploadProductPhotoAsync(IFormFile imageFile,Product product);
    }
}
