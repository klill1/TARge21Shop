using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System.Xml.Linq;
using TARge21Shop.ApplicationServices.Services;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.Models.Car;

namespace TARge21Shop.Controllers
{
    public class CarsController : Controller
    {
        private readonly TARge21ShopContext _context;
        private readonly ICarServices _carsServices;
        private readonly IFilesServices _filesServices;

        public CarsController
            (
            TARge21ShopContext context,
            ICarServices carsServices,
            IFilesServices filesServices
            )
        {
            _context = context;
            _carsServices = carsServices;
            _filesServices = filesServices;
        }

        public IActionResult Index()
        {
            var result = _context.Cars
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    EnginePower = x.EnginePower,
                    Mileage = x.Mileage,
                    FuelType = x.FuelType,
                    FuelConsumption = x.FuelConsumption,
                    BuiltDate = x.BuiltDate,

                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel car = new CarCreateUpdateViewModel();

            return View("CreateUpdate", car);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
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
                BuiltDate = vm.BuiltDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CarId = x.CarId,
                }).ToArray()
            };

            var result = await _carsServices.Create(dto);

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);

        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _carsServices.GetAsync(id);

            if (car is null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new ImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarCreateUpdateViewModel();

            vm.Id = car.Id;
            vm.Name = car.Name;
            vm.Type = car.Type;
            vm.EnginePower = car.EnginePower;
            vm.Weight = car.Weight;
            vm.Lenght = car.Lenght;
            vm.Width = car.Width;
            vm.MaxSpeed = car.MaxSpeed;
            vm.Mileage = car.Mileage;
            vm.FuelType = car.FuelType;
            vm.FuelConsumption = car.FuelConsumption;
            vm.BuiltDate = car.BuiltDate;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
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
                BuiltDate = vm.BuiltDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    CarId = x.CarId,
                }).ToArray()
            };

            var result = await _carsServices.Update(dto);

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carsServices.GetAsync(id);
            if (car is null)
            {
                return NotFound();
            }
            var photos = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new ImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarDetailsViewModel();

            vm.Id = car.Id;
            vm.Name = car.Name;
            vm.Type = car.Type;
            vm.EnginePower = car.EnginePower;
            vm.Weight = car.Weight;
            vm.Lenght = car.Lenght;
            vm.Width = car.Width;
            vm.MaxSpeed = car.MaxSpeed;
            vm.Mileage = car.Mileage;
            vm.FuelType = car.FuelType;
            vm.FuelConsumption = car.FuelConsumption;
            vm.BuiltDate = car.BuiltDate;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            vm.Image.AddRange(photos);

            return View(vm);

        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carsServices.GetAsync(id);
            if (car is null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.CarId == id)
                .Select(y => new ImageViewModel
                {
                    CarId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new CarDeleteViewModel();

            vm.Id = car.Id;
            vm.Name = car.Name;
            vm.Type = car.Type;
            vm.EnginePower = car.EnginePower;
            vm.Weight = car.Weight;
            vm.Lenght = car.Lenght;
            vm.Width = car.Width;
            vm.MaxSpeed = car.MaxSpeed;
            vm.Mileage = car.Mileage;
            vm.FuelType = car.FuelType;
            vm.FuelConsumption = car.FuelConsumption;
            vm.BuiltDate = car.BuiltDate;
            vm.CreatedAt = car.CreatedAt;
            vm.ModifiedAt = car.ModifiedAt;
            vm.Image.AddRange(photos);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _carsServices.Delete(id);

            if (carId is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };

            var image = await _filesServices.RemoveImage(dto);

            if (image is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
