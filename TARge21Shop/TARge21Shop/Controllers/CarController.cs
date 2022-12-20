using Microsoft.AspNetCore.Mvc;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.Models.Car;

namespace TARge21Shop.Controllers
{
    public class CarController : Controller
    {
        private readonly TARge21ShopContext _context;
        private readonly ICarServices _carServices;

        public CarController
            (
                TARge21ShopContext context,
                ICarServices carServices
            )
        {
            _context = context;
            _carServices = carServices;
        }
        public IActionResult Index()
        {
            var result = _context.Cars
                .OrderByDescending(y => y.ProductionDate)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    EnginePower = x.EnginePower,
                    Mileage = x.Mileage,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            CarEditViewModel car = new CarEditViewModel();

            return View("Edit", car);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CarEditViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                EnginePower = vm.EnginePower,
                Weight = vm.Weight,
                Lenght = vm.Lenght,
                Width = vm.Width,
                MaxSpeed = vm.MaxSpeed,
                Mileage = vm.Mileage,
                FuelType = vm.FuelType,
                FuelConsumption = vm.FuelConsumption,
                ProductionDate = vm.ProductionDate

            };

            var result = await _carServices.Add(dto);

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _carServices.GetUpdate(id);
            if (car is null)
            {
                return NotFound();
            }

            var vm = new CarEditViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                Type = car.Type,
                EnginePower = car.EnginePower,
                Weight = car.Weight,
                Lenght = car.Lenght,
                Width = car.Width,
                MaxSpeed = car.MaxSpeed,
                Mileage = car.Mileage,
                FuelType = car.FuelType,
                FuelConsumption = car.FuelConsumption,
                ProductionDate = car.ProductionDate
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarEditViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                EnginePower = vm.EnginePower,
                Weight = vm.Weight,
                Lenght = vm.Lenght,
                Width = vm.Width,
                MaxSpeed = vm.MaxSpeed,
                Mileage = vm.Mileage,
                FuelType = vm.FuelType,
                FuelConsumption = vm.FuelConsumption,
                ProductionDate = vm.ProductionDate
            };

            var result = await _carServices.Update(dto);

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carServices.Delete(id);

            if (carId is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carServices.GetAsync(id);
            if (car is null)
            {
                return NotFound();
            }

            var vm = new CarDetailsViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                Type = car.Type,
                EnginePower = car.EnginePower,
                Weight = car.Weight,
                Lenght = car.Lenght,
                Width = car.Width,
                MaxSpeed = car.MaxSpeed,
                Mileage = car.Mileage,
                FuelType = car.FuelType,
                FuelConsumption = car.FuelConsumption,
                ProductionDate = car.ProductionDate
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carServices.GetAsync(id);
            if (car is null)
            {
                return NotFound();
            }
            var vm = new CarDeleteViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                Type = car.Type,
                EnginePower = car.EnginePower,
                Weight = car.Weight,
                Lenght = car.Lenght,
                Width = car.Width,
                MaxSpeed = car.MaxSpeed,
                Mileage = car.Mileage,
                FuelType = car.FuelType,
                FuelConsumption = car.FuelConsumption,
                ProductionDate = car.ProductionDate
            };

            return View(vm);
        }
    }
}
