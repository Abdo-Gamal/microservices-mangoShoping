using Mango.Web.Models;
using Mango.Web.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.Services.IServices
{
    public interface IProductService:IBaseService
    {
        Task<T> GetAllProductsAsync<T>(string token);
        Task<T> GetProductByIdAsync<T>(int idm, string token);
        Task<T> CreateProductAsync<T>(ProductDto ProductDto, string token);
        Task<T> UpdateProductAsync<T>(ProductDto ProductDto, string token);
        Task<T> DeleteProductAsync<T>(int id, string token);
    }
}
