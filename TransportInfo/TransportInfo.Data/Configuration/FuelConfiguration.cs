using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Configuration
{
    public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
    {
        public void Configure(EntityTypeBuilder<Fuel> builder)
        {
            builder.ToTable("Fuel")
               .Property(f => f.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();

            builder.HasData
           (
                     new Fuel
                     {
                         Id = 10,
                         FuelType = "Electric",
                         FuelTypeGE = "ელექტრო"

                     },
                     new Fuel
                     {

                         Id = 11,
                         FuelType = "Hybrid",
                         FuelTypeGE = "ჰიბრიდი"
                     },
                     new Fuel
                     {

                         Id = 12,
                         FuelType = "Petrol",
                         FuelTypeGE = "ბენზინი"
                     },
                     new Fuel
                     {

                         Id = 13,
                         FuelType = "Gas/Petrol",
                         FuelTypeGE = "გაზი/ბენზინი"
                     }
             );


        }
    }
}
