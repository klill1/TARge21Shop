using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.Models.Spaceship;

namespace TARge21Shop.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly TARge21ShopContext _context;
        private readonly ISpaceshipsServices _spaceshipsServices;

        public SpaceshipsController
            (
                TARge21ShopContext context,
                ISpaceshipsServices spaceshipsServices
            )
        {
            _context = context;
            _spaceshipsServices = spaceshipsServices;
        }

        public IActionResult Index()
        {
            var result = _context.SpaceShips
                .OrderByDescending(y => y.CreatedAt)
                .Select(x => new SpaceshipIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    Passengers = x.Passengers,
                    EnginePower = x.EnginePower
                });
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipCreateUpdateViewModel spaceship = new SpaceshipCreateUpdateViewModel();

            return View("CreateUpdate", spaceship);
        }

        [HttpPost]

        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Crew = vm.Crew,
                Passengers = vm.Passengers,
                CargoWeight = vm.CargoWeight,
                FullTripsCount = vm.FullTripsCount,
                MaintenanceCount = vm.MaintenanceCount,
                LastMaintenance = vm.LastMaintenance,
                EnginePower = vm.EnginePower,
                MaidenLaunch = vm.MaidenLaunch,
                BuiltDate = vm.BuiltDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId,
                }).ToArray()
            };

            var result = await _spaceshipsServices.Create(dto);

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }


        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var spaceship = await _spaceshipsServices.GetAsync(id);
            if (spaceship is null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    SpaceshipId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = String.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new SpaceshipCreateUpdateViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.Crew = spaceship.Crew;
            vm.Passengers = spaceship.Passengers;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.FullTripsCount = spaceship.FullTripsCount;
            vm.MaintenanceCount = spaceship.MaintenanceCount;
            vm.LastMaintenance = spaceship.LastMaintenance;
            vm.EnginePower = spaceship.EnginePower;
            vm.MaidenLaunch = spaceship.MaidenLaunch;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);



            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SpaceshipCreateUpdateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Crew = vm.Crew,
                Passengers = vm.Passengers,
                CargoWeight = vm.CargoWeight,
                FullTripsCount = vm.FullTripsCount,
                MaintenanceCount = vm.MaintenanceCount,
                LastMaintenance = vm.LastMaintenance,
                EnginePower = vm.EnginePower,
                MaidenLaunch = vm.MaidenLaunch,
                BuiltDate = vm.BuiltDate,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    SpaceshipId = x.SpaceshipId,
                }).ToArray()
            };

            var result = await _spaceshipsServices.Update(dto);

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }      

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var spaceship = await _spaceshipsServices.GetAsync(id);
            if (spaceship is null)
            {
                return NotFound();
            }
            var photos = await _context.FileToDatabases
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    SpaceshipId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new SpaceshipDetailsViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.Type = spaceship.Type;
            vm.Crew = spaceship.Crew;
            vm.Passengers = spaceship.Passengers;
            vm.CargoWeight = spaceship.CargoWeight;
            vm.FullTripsCount = spaceship.FullTripsCount;
            vm.MaintenanceCount = spaceship.MaintenanceCount;
            vm.LastMaintenance = spaceship.LastMaintenance;
            vm.EnginePower = spaceship.EnginePower;
            vm.MaidenLaunch = spaceship.MaidenLaunch;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;
            vm.Image.AddRange(photos);



            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var spaceship = await _spaceshipsServices.GetAsync(id);
            if (spaceship is null)
            {
                return NotFound();
            }

            var vm = new SpaceshipDeleteViewModel()
            {
                Id = spaceship.Id,
                Name = spaceship.Name,
                Type = spaceship.Type,
                Crew = spaceship.Crew,
                Passengers = spaceship.Passengers,
                CargoWeight = spaceship.CargoWeight,
                FullTripsCount = spaceship.FullTripsCount,
                MaintenanceCount = spaceship.MaintenanceCount,
                LastMaintenance = spaceship.LastMaintenance,
                EnginePower = spaceship.EnginePower,
                MaidenLaunch = spaceship.MaidenLaunch,
                BuiltDate = spaceship.BuiltDate,
                CreatedAt = spaceship.CreatedAt,
                ModifiedAt = spaceship.ModifiedAt

            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceshipId = await _spaceshipsServices.Delete(id);

            if (spaceshipId is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
