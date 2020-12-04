using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Configuration
{
    public class TransportPersonConfiguration : IEntityTypeConfiguration<TransportPerson>
    {
        public void Configure(EntityTypeBuilder<TransportPerson> builder)
        {
            builder.ToTable("TransportPerson")
                .HasKey(t => new { t.TransportId, t.PersonId });
                 
            builder.ToTable("TransportPerson")
               .Property(c => c.HolderStatus)               
               .IsRequired();

            builder.HasOne(t => t.Transport)
                .WithMany(tp => tp.TransportPersons)
                .HasForeignKey(t => t.TransportId);

            builder.HasOne(p => p.Person)
                .WithMany(tp => tp.TransportPersons)
                .HasForeignKey(p => p.PersonId);



            builder.HasData
             (
                       new TransportPerson
                       {
                           TransportId = Guid.Parse("08277147-51bb-4b17-bebe-05ad4942435c"),
                           PersonId = Guid.Parse("08277147-51bb-4b17-bebe-05ad4942436c"),
                           HolderStatus = true
                       },


                       new TransportPerson
                       {
                           TransportId = Guid.Parse("08277147-51bb-4b17-bebe-05ad4942445c"),
                           PersonId = Guid.Parse("d737f21d-e39f-49fb-9b58-98ce7c1386dd"),
                           HolderStatus = true
                       },
                       new TransportPerson
                       {
                           TransportId = Guid.Parse("08277147-51bb-4b17-bebe-05ad4942435c"),
                           PersonId = Guid.Parse("d737f21d-e39f-49fb-9b58-98ce7c1386dd"),
                           HolderStatus = false
                       }

               );

        }
    }

}
