using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.API.DtoModels.PersonDtos;
using TransportInfo.Domain.Entities;

namespace TransportInfo.API.DtoModels.TransportPersonDtos
{
    public class TransportPersonWithoutDetailsDto
    {        
        public PersonWithoutDetailsDto Person { get; set; }
        public bool HolderStatus { get; set; }
    }
}
