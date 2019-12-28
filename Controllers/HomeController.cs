using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AkwaabaCrafts.Models;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using AkwaabaCrafts.Services;

namespace AkwaabaCrafts.Controllers
{
    public class HomeController : Controller
    {

        public IWebHostEnvironment WebHostEnvironment { get; }
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        [Route("api/products")]
        [HttpGet]
        public IEnumerable<Products> GetProducts()
        {
            using(var jsonFileReader = System.IO.File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Products[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        [Route("api/getproducts")]
        [HttpGet]
        public IEnumerable<Products> GetProd(){
            ProductJsonService productJson = new ProductJsonService();
            return productJson.GetProducts();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
