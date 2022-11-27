using Mango.web.Models;
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

        public HomeController(ILogger<HomeController> logger, IProductService ProductService)
        {
            _logger = logger;
            _ProductService = ProductService;
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