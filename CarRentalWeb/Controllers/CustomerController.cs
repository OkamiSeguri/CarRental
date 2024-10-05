using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject;

namespace CarRentalWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CarRentalAPI");
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            var members = await _httpClient.GetFromJsonAsync<List<Customer>>("Customer");
            return View(members);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"Customer/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FristName,LastName,Address,City,PhoneNumber,Email,Password,Type")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("Customer", customer);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Create successfully!";

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Unable to create customer.");
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"Customer/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FristName,LastName,Address,City,PhoneNumber,Email,Password,Type")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"Csutomer/{id}", customer);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Edit successfully!";

                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Unable to update member.");
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"Customer/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"Customer/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Delete successfully!";

                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
    }
}
