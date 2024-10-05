using BusinessObject;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarRentalWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CarRentalAPI");
        }

        #region Login

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View(new LoginDTO());
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please provide both email and password.";
                return View(dto);
            }

            // Send login request to the API
            var response = await _httpClient.PostAsJsonAsync("Customer/Login", dto);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginDTO>();

                // Save session information
                HttpContext.Session.SetString("Email", result.Email);
                HttpContext.Session.SetInt32("Type", result.Type);
                HttpContext.Session.SetInt32("CustomerId", result.CustomerId);
                HttpContext.Session.SetString("Password", result.Password);
                Console.WriteLine(result);
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Invalid Email or Password! {errorMessage}";
                return View(dto);
            }
        }

        #endregion

        #region Register

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Type = 1;

                var response = await _httpClient.PostAsJsonAsync("Customer", customer);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Login");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();

                    var errorModelState = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(errorResponse);

                    foreach (var error in errorModelState)
                    {
                        foreach (var message in error.Value)
                        {
                            ModelState.AddModelError(error.Key, message);
                        }
                    }
                }
            }
            return View(customer);
        }

        #endregion

        #region Logout

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            // Clear session data
            HttpContext.Session.Remove("Type");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region UpdateProfile

        // GET: /Account/UpdateProfile/{id}
        public async Task<IActionResult> UpdateProfile(int id)
        {
            var customer = await _httpClient.GetFromJsonAsync<Customer>($"Customer/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer); // Return the customer to the update view
        }

        // POST: /Account/UpdateProfile/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(int id, [Bind("CustomerId,FirstName,LastName,Address,City,PhoneNumber,Email,Password,Type")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"Customer/{id}", customer);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction(nameof(Index)); // Redirect to home page after a successful update
                }

                ModelState.AddModelError(string.Empty, "Unable to update customer.");
            }
            return View(customer); // Return the customer object to the view if update fails
        }

        #endregion
    }
}
