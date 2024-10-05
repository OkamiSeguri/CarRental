using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CarDAO : SingletonBase<CarDAO>
    {
        public async Task<IEnumerable<Car>> GetCarsAll()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
        }

        public async Task<IEnumerable<Car>> SearchCars(string searchQuery, decimal? maxPrice)
        {
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(c => c.Brand.Contains(searchQuery) || c.Model.Contains(searchQuery));
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(c => c.Price <= maxPrice.Value);
            }

            return await query.ToListAsync();
        }

        public async Task AddCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCar(Car car)
        {
            var existingCar = await GetCarById(car.CarId);
            if (existingCar != null)
            {
                _context.Entry(existingCar).CurrentValues.SetValues(car);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCar(int id)
        {
            var car = await GetCarById(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }

        
        public async Task<List<Car>> GetCarsByType(string type)
        {
            return await _context.Cars
                .Where(c => c.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<List<Rental>> GetRentalsByDate(DateTime date)
        {
            return await _context.Rentals
                .Where(r => r.StartDate.Date == date.Date)
                .ToListAsync();
        }

    }

}
