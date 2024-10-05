using BusinessObject;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentingAPI.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized("Invalid email or password.");
            }

            var customer = await _customerRepository.GetCustomerByEmailAndPassword(dto.Email, dto.Password);

            if (customer == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Return DTO response
            var responseDto = new LoginDTO
            {
                Email = customer.Email,
                Type = customer.Type,
                CustomerId = customer.CustomerId,
                Password = customer.Password,
            };

            return Ok(responseDto);
        }

        // GET: odata/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerRepository.GetAllCustomers();
            return Ok(customers);
        }

        // GET: odata/customer/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id); 
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: odata/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            await _customerRepository.AddCustomer(customer); 
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerId }, customer);
        }

        // PUT: odata/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            var existingCustomer = await _customerRepository.GetCustomerById(id); 
            if (existingCustomer == null)
            {
                return NotFound();
            }

            await _customerRepository.UpdateCustomer(customer); 
            return NoContent();
        }

        // DELETE: odata/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var existingCustomer = await _customerRepository.GetCustomerById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            await _customerRepository.DeleteCustomer(id); 
            return NoContent();
        }
    }
}
