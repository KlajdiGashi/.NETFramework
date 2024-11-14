using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using NZWalks.UI.Models.DTO;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace NZWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();
            try
            {
                // Get All Regions From Web API
                var client = httpClientFactory.CreateClient();
 
                var httpResponseMessage = await client.GetAsync("https://localhost:7287/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {

                // Log the exception
            }

            return View(response);
        }

        [HttpGet]

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7287/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), System.Text.Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            
            httpResponseMessage.EnsureSuccessStatusCode();

            var response =await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }
            else
            {
                return View();
            }

        }
    }
}
