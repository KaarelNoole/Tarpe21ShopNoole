using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarpe21ShopNoole.Core.Domain;

namespace Tarpe21ShopNoole.Data
{
    public class Tarpe21ShopNooleContext : DbContext
    {
        public Tarpe21ShopNooleContext(DbContextOptions<Tarpe21ShopNooleContext> options) : base(options) { }

        public DbSet<SpaceShip> spaceShips { get; set; }
        public DbSet<FileToDatabase> FilesToDatabase { get; set; }
        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<FileToApi> FilesToApi { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
