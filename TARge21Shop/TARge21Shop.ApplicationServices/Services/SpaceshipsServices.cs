using Microsoft.EntityFrameworkCore;
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
    public class SpaceshipsServices : ISpaceshipsServices
    {
        private readonly TARge21ShopContext _context;
        private readonly IFilesServices _files;
        public SpaceshipsServices
            (
                TARge21ShopContext context,
                IFilesServices files                            
            )
        {
            _context = context;
            _files = files;
        }

        public async Task<Spaceship> Create(SpaceshipDto dto)
        {
            Spaceship spaceship = new Spaceship();
            FileToDatabase file = new FileToDatabase();

            spaceship.Id = Guid.NewGuid();
            spaceship.Name = dto.Name;
            spaceship.Type = dto.Type;
            spaceship.Crew = dto.Crew;
            spaceship.Passengers = dto.Passengers;
            spaceship.CargoWeight = dto.CargoWeight;
            spaceship.FullTripsCount = dto.FullTripsCount;
            spaceship.MaintenanceCount = dto.MaintenanceCount;
            spaceship.LastMaintenance = dto.LastMaintenance;
            spaceship.EnginePower = dto.EnginePower;
            spaceship.MaidenLaunch = dto.MaidenLaunch;
            spaceship.BuiltDate = dto.BuiltDate;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;

            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, spaceship);
            }

          
            await _context.SpaceShips.AddAsync(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;
        }

        public async Task<Spaceship> Update(SpaceshipDto dto)
        {
            var domain = new Spaceship()
            {
                Id = dto.Id,
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
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now

            };

            if (dto.Files != null)
            {
                _files.UploadFilesToDatabase(dto, domain);
            }

            _context.SpaceShips.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Spaceship> Delete(Guid id)
        {
            var spaceshipId = await _context.SpaceShips
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.SpaceShips.Remove(spaceshipId);
            await _context.SaveChangesAsync();

            return spaceshipId;
        }

        public async Task<Spaceship> GetAsync(Guid id)
        {
            var result = await _context.SpaceShips
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

    }
}
