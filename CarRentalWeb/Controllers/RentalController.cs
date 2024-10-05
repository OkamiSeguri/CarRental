using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using System.Net.Http;

namespace CarRentalWeb.Controllers
{
    public class RentalController : Controller
    {
        private readonly HttpClient _httpClient;

        public RentalController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CarRentalAPI");
        }

        // GET: Rental
        public async Task<IActionResult> Index()
        {
            var orders = await _httpClient.GetFromJsonAsync<List<Rental>>("Rental");
            return View(orders);
        }
        // GET: Rental/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var rental = await _httpClient.GetFromJsonAsync<Rental>($"Rental/{id}");
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }
        public async Task<IActionResult> DetailRental(int RentalId)
        {
            var sessionRentalId = HttpContext.Session.GetInt32("RentalId");
            Console.WriteLine($"Session RentalId: {sessionRentalId}");
            Console.WriteLine($"Method Parameter RentalId: {RentalId}");

            // Chắc chắn RentalId không phải là 0 hoặc null
            if (sessionRentalId == null || sessionRentalId == 0)
            {
                return BadRequest("RentalId is not set correctly in the session.");
            }

            var response = await _httpClient.GetAsync($"Rental/{sessionRentalId}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Failed to get Rental. Status code: {response.StatusCode}");
                return NotFound();
            }

            var rentals = await response.Content.ReadFromJsonAsync<List<Rental>>();
            if (rentals == null || !rentals.Any())
            {
                return NotFound();
            }

            return View(rentals);
        }


        // GET: Rental/Create
        public async Task<IActionResult> Create()
        {
            var rentalResponse = await _httpClient.GetAsync("Rental");
            if (rentalResponse.IsSuccessStatusCode)
            {
                var rentals = await rentalResponse.Content.ReadFromJsonAsync<List<Rental>>();
                ViewBag.MemberIdList = rentals?.Select(m => m.RentalId).ToList() ?? new List<int>();
            }
            else
            {
                ViewBag.RentalIdList = new List<int>();
                ModelState.AddModelError(string.Empty, "Unable to load rentals.");
            }
            return View();
        }

        // POST: Rental/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Rental", rental);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Create successfully!";

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Unable to create rental.");
            }
            var rentalResponse = await _httpClient.GetAsync("Rental");
            if (rentalResponse.IsSuccessStatusCode)
            {
                var rentals = await rentalResponse.Content.ReadFromJsonAsync<List<Rental>>();
                ViewBag.RentalIdList = rentals?.Select(m => m.RentalId).ToList() ?? new List<int>();
            }
            else
            {
                ViewBag.RentalIdList = new List<int>();
                ModelState.AddModelError(string.Empty, "Unable to load rentals.");
            }
            return View(rental);
        }

        // GET: Rental/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var rental = await _httpClient.GetFromJsonAsync<Rental>($"Rental/{id}");
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: Rental/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rental rental)
        {
            if (id != rental.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"Rental/{id}", rental);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Edit successfully!";

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Unable to update rental.");
            }
            return View(rental);
        }

        // GET: Rental/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var rental = await _httpClient.GetFromJsonAsync<Rental>($"Rental/{id}");
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: Rental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Rental/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete successfully!";

                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
