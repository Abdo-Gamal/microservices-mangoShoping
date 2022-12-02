﻿using Mango.web.Models;
using Mango.web.Services.IServices;
using Mango.Web;
using Mango.Web.Models;
using Mango.Web.Services;

namespace Mango.web.Services
{
    public class CartService : BaseService, ICartService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<T> AddToCartAsnyc<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.ShoppingCartAPIBase + "/api/products",
                AccessToken = token
            });
        }

        public Task<T> GetCartByUserIdAsnyc<T>(string userId, string token = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> RemoveFromCartAsnyc<T>(int CartId, string token = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateCartAsnyc<T>(CartDto cartDto, string token = null)
        {
            throw new NotImplementedException();
        }
    }
}
