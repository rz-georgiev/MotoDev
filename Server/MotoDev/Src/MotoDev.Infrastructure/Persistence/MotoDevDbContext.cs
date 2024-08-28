using MotoDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;

namespace MotoDev.Infrastructure.Persistence
{
    public class MotoDevDbContext(DbContextOptions<MotoDevDbContext> options) : DbContext(options)
    {
        public DbSet<Brand> Brands { get; set; }

        public DbSet<BrandModel> BrandModels { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarType> CarTypes { get; set; }
        
        public DbSet<Client> Clients { get; set; }
        
        public DbSet<ClientCar> ClientCars { get; set; }

        public DbSet<ClientCarRepair> ClientCarRepairs { get; set; }

        public DbSet<EngineType> EngineTypes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<Repair> Repairs { get; set; }

        public DbSet<RepairShop> RepairShops { get; set; }

        public DbSet<RepairShopClient> RepairShopClients { get; set; }

        public DbSet<RepairShopUser> RepairShopUsers { get; set; }

        public DbSet<RepairType> RepairType { get; set; }

        public DbSet<Role> Roles { get; set; }
        
        public DbSet<TransmissionType> TransmissionTypes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles{ get; set; }

    }
}