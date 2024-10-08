﻿using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllCars();
        Task<Car> GetCarById(int id);
        Task AddCar(Car car);
        Task UpdateCar(Car car);
        Task DeleteCar(int id);
    }
}
