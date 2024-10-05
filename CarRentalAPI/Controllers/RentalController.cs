using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace CarRentingAPI.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalController(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        // GET: odata/rental
        [HttpGet]
        public ActionResult<IEnumerable<Rental>> GetRentals()
        {
            var rentals = _rentalRepository.GetAllRentals();
            return Ok(rentals);
        }

        // GET: odata/rental/{id}
        [HttpGet("{id}")]
        public ActionResult<Rental> GetRental(int id)
        {
            var rental = _rentalRepository.GetRental(id);
            if (rental == null)
            {
                return NotFound();
            }
            return Ok(rental);
        }

        // POST: odata/rental
        [HttpPost]
        public ActionResult<Rental> CreateRental([FromBody] Rental rental)
        {
            if (rental == null)
            {
                return BadRequest();
            }

            _rentalRepository.AddRental(rental);
            return CreatedAtAction(nameof(GetRental), new { id = rental.RentalId }, rental);
        }

        // PUT: odata/rental/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateRental(int id, [FromBody] Rental rental)
        {
            if (id != rental.RentalId)
            {
                return BadRequest();
            }

            var existingRental = _rentalRepository.GetRental(id);
            if (existingRental == null)
            {
                return NotFound();
            }

            _rentalRepository.UpdateRental(rental);
            return NoContent();
        }

        // DELETE: odata/rental/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteRental(int id)
        {
            var existingRental = _rentalRepository.GetRental(id);
            if (existingRental == null)
            {
                return NotFound();
            }

            _rentalRepository.DeleteRental(id);
            return NoContent();
        }
    }
}
