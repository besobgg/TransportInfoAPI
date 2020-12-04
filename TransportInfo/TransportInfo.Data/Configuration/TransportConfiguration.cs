using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Configuration
{
    public class TransportConfiguration : IEntityTypeConfiguration<Transport>
    {
        public void Configure(EntityTypeBuilder<Transport> builder)
        {
            builder.HasData
            (
                      new Transport
                      {
                          TransportId = Guid.Parse("08277147-51bb-4b17-bebe-05ad4942435c"),
                          VinCode = "34353453THFDHJ87Y6",
                          RegistrationNumber = "AA-200-AA",
                          ColorId = 10,
                          ManufactureDate = DateTime.Parse("09/04/1990"),
                          ManufacturerId=10,
                          ModelId =10,
                          FuelId=10,

                      },
                      new Transport
                      {

                          TransportId = Guid.Parse("08277147-51bb-4b17-bebe-05ad4942445c"),
                          VinCode = "34353453THFDHJ87Y6",
                          RegistrationNumber = "BB-200-AA",
                          ColorId = 11,
                          ManufactureDate = DateTime.Parse("09/04/2000"),
                          ManufacturerId = 11,
                          ModelId = 11,
                          FuelId = 12,
                      },
                      new Transport
                      {
                          TransportId = Guid.Parse("08277147-51bb-4b17-bebe-05ad4943435c"),
                          VinCode = "34353453THFDHJ87Y6",
                          RegistrationNumber = "CC-200-AA",
                          ColorId = 12,
                          ManufactureDate = DateTime.Parse("09/04/1999"),
                          ManufacturerId = 12,
                          ModelId = 11,
                          FuelId = 13,
                      }
              );



        }
    }
}
