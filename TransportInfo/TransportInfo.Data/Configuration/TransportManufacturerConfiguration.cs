using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Configuration
{

    public class TransportManufacturerConfiguration : IEntityTypeConfiguration<TransportManufacturer>
    {
        public void Configure(EntityTypeBuilder<TransportManufacturer> builder)
        {
            builder.ToTable("TransportManufacturer")
               .Property(tm => tm.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();

            builder.HasMany(tm => tm.TransportModels)
                .WithOne(t => t.Manufacturer)
                .HasForeignKey(tm => tm.ManufacturerId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData
           (
                     new TransportManufacturer
                     {
                         Id = 10,
                         ManufacturerName = "MERCEDES-BENZ",
                         ManufacturerNameGE="მერსედეს-ბენც"
                        

                     },
                     new TransportManufacturer
                     {
                         Id = 11,
                         ManufacturerName = "BMW",
                         ManufacturerNameGE = "ბმვ"
                     },
                     new TransportManufacturer
                     {

                         Id = 12,
                         ManufacturerName = "AUDI",
                         ManufacturerNameGE = "აუდი"
                     }
             );


        }
    }
}
