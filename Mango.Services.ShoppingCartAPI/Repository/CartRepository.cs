using AutoMapper;
using Mango.Services.ShoppingCartAPI.DbContexts;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public  async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userId)
        {
            var CartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(
                u => u.UserId == userId);

            if (CartHeaderFromDb != null)
            {
                _db.CartDetails.RemoveRange(_db.CartDetails.Where(
                    u=>u.CartHeaderId==CartHeaderFromDb.CartHeaderId));
                _db.CartHeaders.Remove(CartHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {
            Cart cart = _mapper.Map<Cart>(cartDto);

            //check if product  exist in DB ,if  not create it .
            //in front  in each time we add one product so  we  write first or default .
            var prodInDb = _db.Products.FirstOrDefaultAsync(
                u=>u.ProductId==cartDto.CartDetails.FirstOrDefault().ProductId);
            if (prodInDb == null)
            {
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }
            //check if header is null 
            var CartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(
                u=>u.CartHeaderId==cart.CartHeader.CartHeaderId);
            if (CartHeaderFromDb == null)
            {
                //cread header and details 
                _db.CartHeaders.Add(cart.CartHeader);
                await _db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                // in cartdetails  exist product object so ef try to add product with same id
                cart.CartDetails.FirstOrDefault().Product = null;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //if header not null 
                // check if details  has same product
                var CartDetailFromDb =await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == CartHeaderFromDb.CartHeaderId
                    ) ;

                if (CartDetailFromDb == null)
                {
                    //crate cartDetails 
                    cart.CartDetails.FirstOrDefault().CartHeaderId = CartHeaderFromDb.CartHeaderId;
                    // in cartdetails  exist product object so ef try to add product with same id
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //update the count / cart details
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count+= CartDetailFromDb.Count;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();

                }
            }
            return  _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {
            throw new NotImplementedException();
        }
    }
}
