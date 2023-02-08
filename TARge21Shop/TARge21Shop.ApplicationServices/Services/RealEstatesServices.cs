using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;

namespace TARge21Shop.ApplicationServices.Services
{

    public class RealEstatesServices : IRealEstatesServices
    {
        private readonly TARge21ShopContext _context;
        private readonly IFilesServices _files;
        public RealEstatesServices
            (
                TARge21ShopContext context,
                IFilesServices files
            )
        {
            _context = context;
            _files = files;
        }

        //public IEnumerable<RealEstate> GetAllRealEstates()
        //{
        //       var result = _context.RealEstates
        //            .OrderByDescending(y => y.CreatedAt)
        //            .Select(x => new RealEstate
        //            {
        //                Id = x.Id,
        //                Price = x.Price,
        //            });

        //    return result;

        //}

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            RealEstate realEstate = new();
            //FileToDatabase file = new FileToDatabase();

            realEstate.Id = Guid.NewGuid();
            realEstate.Address = dto.Address;
            realEstate.City = dto.City;
            realEstate.Region = dto.Region;
            realEstate.PostalCode = dto.PostalCode; 
            realEstate.Country = dto.Country;
            realEstate.Phone = dto.Phone;
            realEstate.Fax = dto.Fax;
            realEstate.Size = dto.Size;
            realEstate.Floor = dto.Floor;
            realEstate.Price = dto.Price;
            realEstate.RoomCount = dto.RoomCount;
            realEstate.CreatedAt = DateTime.Now;
            realEstate.ModifiedAt = DateTime.Now;

            //if (dto.Files != null)
            //{
            //    _files.UploadFilesToDatabase(dto, realEstate);
            //}

            await _context.RealEstates.AddAsync(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }
        
        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            var domain = new RealEstate()
            {
                Id = dto.Id,
                Address = dto.Address,
                City = dto.City,
                Region = dto.Region,
                PostalCode = dto.PostalCode,
                Country = dto.Country,
                Phone = dto.Phone,
                Fax = dto.Fax,
                Size = dto.Size,
                Floor = dto.Floor,
                Price = dto.Price,
                RoomCount = dto.RoomCount,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now
            };

            //if (dto.Files != null)
            //{
            //    _files.UploadFilesToDatabase(dto, domain);
            //}

            _context.RealEstates.Update(domain);
            await _context.SaveChangesAsync();
            
            return domain;
        }
        
        public async Task<RealEstate> Delete(Guid id)
        {
            var realestateId = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            //var images = await _context.FileToDatabases
            //    .Where(x => x.RealEstateId == id)
            //    .Select(y => new FileToDatabaseDto
            //    {
            //        Id = y.Id,
            //        ImageTitle = y.ImageTitle,
            //        RealEstateId = y.RealEstateId,
            //    })
            //    .ToArrayAsync();

            //await _files.RemoveImagesFromDatabase(images);
            _context.RealEstates.Remove(realestateId);
            await _context.SaveChangesAsync();
            
            return realestateId;
        }
        
        public async Task<RealEstate> GetAsync(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);
            
            return result;
        }
    }
}
