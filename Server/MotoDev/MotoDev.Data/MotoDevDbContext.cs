﻿using EngineExpert.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MotoDev.Data
{
    public class MotoDevDbContext(DbContextOptions<MotoDevDbContext> options) : DbContext(options)
    {
        public DbSet<Car> Cars { get; set; }

        public DbSet<CarType> CarTypes { get; set; }
        
        public DbSet<Client> Clients { get; set; }
        
        public DbSet<ClientCar> ClientCars { get; set; }

        public DbSet<EngineType> EngineTypes { get; set; }

        public DbSet<Repair> Repairs { get; set; }

        public DbSet<RepairType> RepairType { get; set; }

        public DbSet<Role> Roles { get; set; }
        
        public DbSet<TransmissionType> TransmissionTypes { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles{ get; set; }


    }
}