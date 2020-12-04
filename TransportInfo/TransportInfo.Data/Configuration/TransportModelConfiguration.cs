using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Configuration
{

    public class TransportModelConfiguration : IEntityTypeConfiguration<TransportModel>
    {
        public void Configure(EntityTypeBuilder<TransportModel> builder)
        {
            builder.ToTable("TransportModel")
               .Property(tm => tm.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();

            builder.HasMany(t => t.Transports)
                .WithOne(tm => tm.Model)
                .HasForeignKey(t => t.ModelId)
                .OnDelete(DeleteBehavior.Restrict);
                


            builder.HasData
           (
                     new TransportModel
                     {
                         Id = 10,
                         ModelName = "E200",
                         ModelNameGE = "ე200",
                         ManufacturerId=10
                         


                     },
                     new TransportModel
                     {
                         Id = 11,
                         ModelName = "325",
                         ModelNameGE = "325",
                         ManufacturerId = 11
                     },
                     new TransportModel
                     {

                         Id = 12,
                         ModelName = "A8",
                         ModelNameGE = "ა8",
                         ManufacturerId = 12
                     }
             );


        }
    }
}
