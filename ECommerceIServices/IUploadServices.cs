using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceIServices
{
    public interface IUploadServices
    {
        Task UploadingProductPhoto(IFormFile imageFile,int productId);
    }
}
