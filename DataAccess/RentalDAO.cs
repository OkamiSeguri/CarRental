using BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class RentalDAO : SingletonBase<RentalDAO>
    {
        private readonly List<Rental> _rentals = new List<Rental>();
        private int _nextId = 1;

        public async Task<Rental> GetRentalById(int id)
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(c => c.RentalId == id);
            return rental;
        }

        public void AddRental(Rental rental)
        {
            rental.RentalId = _nextId++;
            _rentals.Add(rental);
        }

        public void UpdateRental(Rental rental)
        {
            var existingRental = _rentals.FirstOrDefault(r => r.RentalId == rental.RentalId);
            if (existingRental != null)
            {
                existingRental.CarId = rental.CarId;
                existingRental.FirstName = rental.FirstName;
                existingRental.LastName = rental.LastName;
                existingRental.StartDate = rental.StartDate;
                existingRental.EndDate = rental.EndDate;
                existingRental.TotalAmount = rental.TotalAmount;
            }
        }

        public void DeleteRental(int id)
        {
            var rental = _rentals.FirstOrDefault(r => r.RentalId == id);
            if (rental != null)
            {
                _rentals.Remove(rental);
            }
        }

        public Rental GetRental(int id)
        {
            return _rentals.FirstOrDefault(r => r.RentalId == id);
        }

        public IEnumerable<Rental> GetAllRentals()
        {
            return _rentals;
        }
    }
}
