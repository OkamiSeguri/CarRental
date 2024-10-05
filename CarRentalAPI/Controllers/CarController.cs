using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace CarRentingAPI.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // GET: odata/car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var cars = await _carRepository.GetAllCars();
            return Ok(cars);
        }

        // GET: odata/car/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _carRepository.GetCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        // POST: odata/car
        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar([FromBody] Car car)
        {
            if (car == null)
            {
                return BadRequest("Car data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Save the car to the database
            await _carRepository.AddCar(car);
            return CreatedAtAction(nameof(GetCar), new { id = car.CarId }, car);
        }

        // PUT: odata/car/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            var existingCar = await _carRepository.GetCarById(id);
            if (existingCar == null)
            {
                return NotFound();
            }

            await _carRepository.UpdateCar(car);
            return NoContent();
        }

        // DELETE: odata/car/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var existingCar = await _carRepository.GetCarById(id);
            if (existingCar == null)
            {
                return NotFound();
            }

            await _carRepository.DeleteCar(id);
            return NoContent();
        }
    }
}
