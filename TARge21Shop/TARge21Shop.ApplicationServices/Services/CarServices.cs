using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Domain.Car;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;

namespace TARge21Shop.ApplicationServices.Services
{
    public class CarServices : ICarServices
    {
        private readonly TARge21ShopContext _context;
        public CarServices(TARge21ShopContext context)
        {
            _context = context;
        }

        public async Task<Car> Add(CarDto dto)
        {
            var domain = new Car()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Type = dto.Type,
                EnginePower = dto.EnginePower,
                Weight = dto.Weight,
                Lenght = dto.Lenght,
                Width = dto.Width,
                MaxSpeed = dto.MaxSpeed,
                Mileage = dto.Mileage,
                FuelType = dto.FuelType,
                FuelConsumption = dto.FuelConsumption,
                ProductionDate = dto.ProductionDate,
            };

            await _context.Cars.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
            
        }

        public async Task<Car> Update(CarDto dto)
        {
            var domain = new Car()
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                EnginePower = dto.EnginePower,
                Weight = dto.Weight,
                Lenght = dto.Lenght,
                Width = dto.Width,
                MaxSpeed = dto.MaxSpeed,
                Mileage = dto.Mileage,
                FuelType = dto.FuelType,
                FuelConsumption = dto.FuelConsumption,
                ProductionDate = dto.ProductionDate
            };

            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Car> GetUpdate(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Car> Delete(Guid id)
        {
            var carId = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.Cars.Remove(carId);
            await _context.SaveChangesAsync();

            return carId;
        }

        public async Task<Car> GetAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }
}
