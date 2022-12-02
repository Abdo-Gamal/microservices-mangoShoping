using Mango.web.Models;

namespace Mango.web.Services.IServices
{
    public interface ICartService
    {

        Task<T> GetCartByUserIdAsnyc<T>(string userId,string token=null);
        Task<T> AddToCartAsnyc<T>(CartDto cartDto, string token = null);

        Task<T> UpdateCartAsnyc<T>(CartDto cartDto, string token = null);
        //I mean by cartId  is cartDetailsId
        Task<T> RemoveFromCartAsnyc<T>(int CartId, string token = null);

    }
}
