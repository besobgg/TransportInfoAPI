using Microsoft.EntityFrameworkCore;
using System;
using TransportInfo.Data.Configuration;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Contexts
{
    public class TransportInfoContext : DbContext
    {
        public TransportInfoContext(DbContextOptions<TransportInfoContext> options)
           : base(options)
        {
        }

       
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<TransportPerson> TransportPersons { get; set; }
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<TransportManufacturer> TransportManufacturers { get; set; }
        public DbSet<TransportModel> TransportModels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new ColorConfiguration());
            modelBuilder.ApplyConfiguration(new FuelConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new TransportConfiguration());
            modelBuilder.ApplyConfiguration(new TransportModelConfiguration());
            modelBuilder.ApplyConfiguration(new TransportManufacturerConfiguration());            
            modelBuilder.ApplyConfiguration(new TransportPersonConfiguration());
        }


    }
}
