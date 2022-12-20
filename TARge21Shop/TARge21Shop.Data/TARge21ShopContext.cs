using Microsoft.EntityFrameworkCore;
using TARge21Shop.Core.Domain.Car;
using TARge21Shop.Core.Domain.Spaceship;

namespace TARge21Shop.Data
{
    public class TARge21ShopContext : DbContext
    {
        public TARge21ShopContext(DbContextOptions<TARge21ShopContext> options) : base(options)
        {
        }

        public DbSet<Spaceship> SpaceShips { get; set; }
        public DbSet<Car> Cars { get; set; }

    }
}
