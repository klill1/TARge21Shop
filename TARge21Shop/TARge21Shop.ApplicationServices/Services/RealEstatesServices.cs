﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARge21Shop.Core.Domain;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;

namespace TARge21Shop.ApplicationServices.Services
{

    public class RealEstatesServices : IRealEstatesServices
    {
        private readonly TARge21ShopContext _context;
        public RealEstatesServices
            (
                TARge21ShopContext context
            )
        {
            _context = context;
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

            await _context.RealEstates.AddAsync(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }
    }
}
