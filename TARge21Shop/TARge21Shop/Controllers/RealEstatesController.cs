using Microsoft.AspNetCore.Mvc;
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

        public RealEstatesController
            (
                IRealEstatesServices realEstatesServices,
                TARge21ShopContext context

            )
        {
            _realEstatesServices = realEstatesServices;
            _context = context;
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
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _realEstatesServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", vm);
        }
    }
}
