using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using CarRentalWeb.Models;

namespace CarRentalWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CarRentalAPI");
        }

        // GET: Home/Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("Car");
            if (response.IsSuccessStatusCode)
            {
                var cars = await response.Content.ReadFromJsonAsync<List<Car>>();
                return View(cars); // Pass the list of cars to the view
            }
            else
            {
                // Handle the error (you might want to log this)
                return View(new List<Car>()); // Return an empty list on error
            }
        }

        // GET: Home/Search
        public async Task<IActionResult> Search(string searchQuery)
        {
            var url = "Car";
            var query = string.Empty;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query += $"?searchQuery={Uri.EscapeDataString(searchQuery)}"; // Escape query
            }

            var fullUrl = url + query;
            var response = await _httpClient.GetAsync(fullUrl);
            if (response.IsSuccessStatusCode)
            {
                var cars = await response.Content.ReadFromJsonAsync<List<Car>>();
                ViewBag.SearchQuery = searchQuery; // Store search query for view
                return View("Index", cars); // Return the Index view with filtered cars
            }
            return View("Index", new List<Car>()); // Return empty list if no cars found
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
