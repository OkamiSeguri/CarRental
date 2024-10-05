using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObject;
using DataAccess;

namespace Repositories
{
    public class CarRepository : ICarRepository
    {
        public async Task<IEnumerable<Car>> GetAllCars()
        {
            return await CarDAO.Instance.GetCarsAll();
        }

        public async Task<Car> GetCarById(int id)
        {
            return await CarDAO.Instance.GetCarById(id);
        }

        public async Task AddCar(Car car)
        {
            await CarDAO.Instance.AddCar(car);
        }

        public async Task UpdateCar(Car car)
        {
            await CarDAO.Instance.UpdateCar(car);
        }

        public async Task DeleteCar(int id)
        {
            await CarDAO.Instance.DeleteCar(id);
        }
    }
}
