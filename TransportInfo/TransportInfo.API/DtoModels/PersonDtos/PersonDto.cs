using TransportInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TransportInfo.API.DtoModels.TransportPersonDtos;

namespace TransportInfo.API.DtoModels.PersonDtos
{
   public class PersonDto
    {

        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Phone { get; set; }
        public ICollection<TransportPersonWithOnlyTransportDto> TransportPersons { get; set; }
    }
}
