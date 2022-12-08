using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TARge21Shop.Core.Domain.Spaceship;
using TARge21Shop.Core.Dto;
using TARge21Shop.Core.ServiceInterface;
using TARge21Shop.Data;

namespace TARge21Shop.ApplicationServices.Services
{
    public class SpaceshipsServices : ISpaceshipsServices
    {
        private readonly TARge21ShopContext _context;
        public SpaceshipsServices(TARge21ShopContext context)
        {
            _context = context;
        }

        public async Task<Spaceship> Add(SpaceshipDto dto)
        {
            var domain = new Spaceship()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Type = dto.Type,
                Crew = dto.Crew,
                Passengers = dto.Passengers,
                CargoWeight = dto.CargoWeight,
                FullTripsCount = dto.FullTripsCount,
                MaintenanceCount = dto.MaintenanceCount,
                LastMaintenance = dto.LastMaintenance,
                EnginePower = dto.EnginePower,
                MaidenLaunch = dto.MaidenLaunch,
                BuiltDate = dto.BuiltDate,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now

            };

            await _context.SpaceShips.AddAsync(domain);
            await _context.SaveChangesAsync();

            return domain;
        }
    }
}
