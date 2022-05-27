using Microsoft.AspNetCore.Mvc;
using PruebaconNav.Models;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace PruebaconNav.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SubirArchivo(string descripcion, IFormFile documento) { 
        
            var cliente = new HttpClient();
            using (var multipartFormContent = new MultipartFormDataContent()) {
                multipartFormContent.Add(new StringContent(descripcion), name: "Descripcion");
                var filestreamContent = new StreamContent(documento.OpenReadStream()); // se recibe el documento
                filestreamContent.Headers.ContentType = new MediaTypeHeaderValue(documento.ContentType);

                multipartFormContent.Add(filestreamContent, name: "Archivo", fileName: documento.FileName);

                var response = await cliente.PostAsync(";http://localhost:5048/api/Documento/Subir", multipartFormContent);
            

                var test= await response.Content.ReadAsStringAsync(); 
            }

            return View("Index");
        
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



    }
}