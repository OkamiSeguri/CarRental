using BusinessObject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> GetRentalById(int id);
        Task AddRental(Rental rental);
        Task<Rental> GetRental(int id);
        Task<IEnumerable<Rental>> GetAllRentals();
        Task UpdateRental(Rental rental);
        Task DeleteRental(int id);
    }
}
