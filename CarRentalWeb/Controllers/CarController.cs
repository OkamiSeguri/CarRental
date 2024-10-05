using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessObject;

namespace CarRentalWeb.Controllers
{
    public class CarController : Controller
    {
        private readonly HttpClient _httpClient;

        public CarController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CarRentalAPI");
        }

        // GET: Car
        public async Task<IActionResult> Index()
        {
            var cars = await _httpClient.GetFromJsonAsync<List<Car>>("Car");
            return View(cars);
        }

        // GET: Car/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var car = await _httpClient.GetFromJsonAsync<Car>($"Car/{id}");
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // GET: Car/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,Brand,Model,Year,Type,Price,Image")] Car car)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Car", car);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Create successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Unable to create car. " + await response.Content.ReadAsStringAsync());
            }
            return View(car);
        }

        // GET: Car/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _httpClient.GetFromJsonAsync<Car>($"Car/{id}");
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,CarName,Image")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"Car/{id}", car);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Edit successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Unable to update car. " + await response.Content.ReadAsStringAsync());
            }
            return View(car);
        }

        // GET: Car/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _httpClient.GetFromJsonAsync<Car>($"Car/{id}");
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Car/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete successfully!";
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Unable to delete car. " + await response.Content.ReadAsStringAsync());
            return View("Error");
        }
    }
}
