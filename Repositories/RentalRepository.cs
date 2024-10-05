using BusinessObject;
using DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly List<Rental> _rentals = new List<Rental>();
        private int _nextId = 1;

        public async Task<Rental> GetRentalById(int id)
        {
            return await RentalDAO.Instance.GetRentalById(id);
        }

        // Create
        public async Task AddRental(Rental rental)
        {
            rental.RentalId = _nextId++;
            _rentals.Add(rental);
            await Task.CompletedTask; // Simulate async operation
        }

        // Read
        public async Task<Rental> GetRental(int id)
        {
            var rental = await Task.FromResult(_rentals.FirstOrDefault(r => r.RentalId == id));
            return rental;
        }

        public async Task<IEnumerable<Rental>> GetAllRentals()
        {
            return await Task.FromResult(_rentals.AsEnumerable());
        }

        // Update
        public async Task UpdateRental(Rental rental)
        {
            var existingRental = await GetRental(rental.RentalId);
            if (existingRental != null)
            {
                existingRental.CarId = rental.CarId;
                existingRental.FirstName = rental.FirstName;
                existingRental.LastName = rental.LastName;
                existingRental.StartDate = rental.StartDate;
                existingRental.EndDate = rental.EndDate;
                existingRental.TotalAmount = rental.TotalAmount;
            }
            await Task.CompletedTask; // Simulate async operation
        }

        // Delete
        public async Task DeleteRental(int id)
        {
            var rental = await GetRental(id);
            if (rental != null)
            {
                _rentals.Remove(rental);
            }
            await Task.CompletedTask; // Simulate async operation
        }

    }
}
