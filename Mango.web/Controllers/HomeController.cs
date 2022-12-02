using Mango.web.Models;
using Mango.web.Services.IServices;
using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mango.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _ProductService;
        private readonly ICartService _CartService;
        public HomeController(ILogger<HomeController> logger, 
            IProductService ProductService, ICartService CartService)
        {
            _logger = logger;
            _ProductService = ProductService;
            _CartService = CartService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new();
            //if action not have  [authoize] attribute then pass token not import 
            var reponse = await _ProductService.GetAllProductsAsync<ResponseDto>("");
            if (reponse != null && reponse.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(reponse.Result));
            }
            return View(list);
        }
        [Authorize]
        public async Task<IActionResult> Details(int ProductId)
        {
            ProductDto model = new();
            //if action not have  [authoize] attribute then pass token not import 
            var reponse = await _ProductService.GetProductByIdAsync<ResponseDto>(ProductId,"");
            if (reponse != null && reponse.IsSuccess)
            {
                model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(reponse.Result));
            }
            return View(model);
        }
        [HttpPost]
        [Authorize]
        [ActionName("Details")]
        public async Task<IActionResult> DetailsPost(ProductDto productDto)
        {
            CartDto cartDto = new()
            {
                CartHeader= new CartHeaderDto (){
                UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };
            CartDetailsDto cartDetails = new CartDetailsDto()
            {
                Count = productDto.Count,
                ProductId = productDto.ProductId,

            };
            var resp = await _ProductService.GetProductByIdAsync<ResponseDto>(productDto.ProductId,"");
            if (resp != null && resp.IsSuccess == true)
            {
                cartDetails.Product = JsonConvert.DeserializeObject<ProductDto>
                    (Convert.ToString( resp.Result));
            }
            List<CartDetailsDto> cartDetailsDtos = new();
            cartDetailsDtos.Add(cartDetails);
            cartDto.CartDetails = cartDetailsDtos;
            var accesToken = await HttpContext.GetTokenAsync("access_token");
            var addToCartResp = await _CartService.
                AddToCartAsnyc<ResponseDto>(cartDto,accesToken);

            if (addToCartResp != null && addToCartResp.IsSuccess == true)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(productDto);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}