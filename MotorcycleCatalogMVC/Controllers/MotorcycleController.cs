using DigitalLibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MotorcycleCatalogMVC.Controllers
{
    public class MotorcycleController : Controller
    {
        private readonly HttpClient _client;
        Uri uri = new("http://localhost:44345/api");
        public MotorcycleController()
        {
            _client = new HttpClient();
            _client.BaseAddress = uri;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<MotorcycleViewModel> motorcycleList = new List<MotorcycleViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress).Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                motorcycleList = JsonConvert.DeserializeObject<List<MotorcycleViewModel>>(data);
            }

            return View(motorcycleList);
        }
    }
}
