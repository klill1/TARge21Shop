using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;
using TARge21Shop.Models.RealEstate;

namespace TARge21Shop.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly IRealEstatesServices _realEstatesServices;
        private readonly TARge21ShopContext _context;
        private readonly IFilesServices _filesServices;

        public RealEstatesController
            (
                IRealEstatesServices realEstatesServices,
                TARge21ShopContext context,
                IFilesServices filesServices

            )
        {
            _realEstatesServices = realEstatesServices;
            _context = context;
            _filesServices = filesServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var result = _context.RealEstates
                 .OrderByDescending(y => y.CreatedAt)
                 .Select(x => new RealEstateIndexViewModel
                 {
                     Id = x.Id,
                     Address = x.Address,
                     City = x.City,
                     Country = x.Country,
                     Size = x.Size,
                     Price = x.Price,
                 });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel vm = new();

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                Size = vm.Size,
                Price = vm.Price,
                Floor = vm.Floor,
                Region = vm.Region,
                Phone = vm.Phone,
                Fax = vm.Fax,
                PostalCode = vm.PostalCode,
                RoomCount = vm.RoomCount,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels.Select(x => new FileToApiDto
                {
                    Id = x.ImageId,
                    ExistingFilePath = x.FilePath,
                    RealEstateId = x.RealEstateId
                }).ToArray()
                
            };

            var result = await _realEstatesServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realestate = await _realEstatesServices.GetAsync(id);
            if (realestate is null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id,
                }).ToArrayAsync();

            var vm = new RealEstateCreateUpdateViewModel();
            
            vm.Id = realestate.Id;
            vm.Address = realestate.Address;
            vm.City = realestate.City;
            vm.Country = realestate.Country;
            vm.Size = realestate.Size;
            vm.Price = realestate.Price;
            vm.Floor = realestate.Floor;
            vm.Region = realestate.Region;
            vm.Phone = realestate.Phone;
            vm.Fax = realestate.Fax;
            vm.PostalCode = realestate.PostalCode;
            vm.RoomCount = realestate.RoomCount;
            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);
            
            return View("CreateUpdate", vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Address = vm.Address,
                City = vm.City,
                Country = vm.Country,
                Size = vm.Size,
                Price = vm.Price,
                Floor = vm.Floor,
                Region = vm.Region,
                Phone = vm.Phone,
                Fax = vm.Fax,
                PostalCode = vm.PostalCode,
                RoomCount = vm.RoomCount,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                FileToApiDtos = vm.FileToApiViewModels
                    .Select(x => new FileToApiDto
                    {
                        Id = x.ImageId,
                        ExistingFilePath = x.FilePath,
                        RealEstateId = x.RealEstateId
                    }).ToArray()    
            };
            
            var result = await _realEstatesServices.Update(dto);

            if (result is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var realestate = await _realEstatesServices.GetAsync(id);
            if (realestate is null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id,
                }).ToArrayAsync();
            
            var vm = new RealEstateDetailsViewModel();
             
            vm.Id = realestate.Id;
            vm.Address = realestate.Address;
            vm.City = realestate.City;
            vm.Country = realestate.Country;
            vm.Size = realestate.Size;
            vm.Price = realestate.Price;
            vm.Floor = realestate.Floor;
            vm.Region = realestate.Region;
            vm.Phone = realestate.Phone;
            vm.Fax = realestate.Fax;
            vm.PostalCode = realestate.PostalCode;
            vm.RoomCount = realestate.RoomCount;
            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);
            
            return View(vm);           

        }
        
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realestate = await _realEstatesServices.GetAsync(id);
            if (realestate is null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.RealEstateId == id)
                .Select(y => new FileToApiViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageId = y.Id,
                }).ToArrayAsync();

            var vm = new RealEstateDeleteViewModel();
            
            vm.Id = realestate.Id;
            vm.Address = realestate.Address;
            vm.City = realestate.City;
            vm.Country = realestate.Country;
            vm.Size = realestate.Size;
            vm.Price = realestate.Price;
            vm.Floor = realestate.Floor;
            vm.Region = realestate.Region;
            vm.Phone = realestate.Phone;
            vm.Fax = realestate.Fax;
            vm.PostalCode = realestate.PostalCode;
            vm.RoomCount = realestate.RoomCount;
            vm.CreatedAt = realestate.CreatedAt;
            vm.ModifiedAt = realestate.ModifiedAt;
            vm.FileToApiViewModels.AddRange(images);
            
            return View(vm);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realestateId = await _realEstatesServices.Delete(id);

            if (realestateId is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> RemoveImage(FileToApiViewModel vm)
        {
            var dto = new FileToApiDto()
            {
                Id = vm.ImageId
            };

            var image = await _filesServices.RemoveImageFromApi(dto);

            if (image is null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
