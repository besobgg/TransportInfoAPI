using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData
            (
                      new Person
                      {
                          PersonId = Guid.Parse("08277147-51bb-4b17-bebe-05ad4942436c"),
                          FirstName = "ბესო",                          
                          LastName = "გოგისვანიძე",                          
                          PersonalNumber = "60001137083",
                          Phone="577977329"
                      },
                      new Person
                      {

                          PersonId = Guid.Parse("d737f21d-e39f-49fb-9b58-98ce7c1386dd"),
                          FirstName = "ვახო",
                          LastName = "კინწურაშვილი",
                          PersonalNumber = "60001137083",
                          Phone = "577977329"
                      },
                      new Person
                      {

                          PersonId = Guid.Parse("9bad0423-2cf9-4a04-8604-b3d14e0f37eb"),
                          FirstName = "ლადო",
                          LastName = "მშვენიერაძე",
                          PersonalNumber = "60001137083",
                          Phone = "577977329"
                      }
              );
        }
    }
}
