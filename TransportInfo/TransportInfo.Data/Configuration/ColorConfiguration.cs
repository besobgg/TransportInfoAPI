using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Configuration
{

    public class ColorConfiguration : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("Color")
               .Property(c => c.Id)
               .ValueGeneratedOnAdd()
               .IsRequired();

            
            builder.HasData
           (
                     new Color
                     {
                         Id = 10,
                         ColorName = "silver",
                         ColorNameGE= "ვერცხლისფერი"
                     },
                     new Color
                     {

                         Id = 11,
                         ColorName = "green",
                         ColorNameGE = "მწვანე"
                     },
                     new Color
                     {

                         Id = 12,
                         ColorName = "red",
                         ColorNameGE = "წითელი"
                     }
             );


        }
    }
}
