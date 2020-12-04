using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.API.DtoModels.PersonDtos;

namespace TransportInfo.API.DtoModels.TransportPersonDtos
{
    public class TransportPersonForCreationDto
    {
        public PersonCreationForTransportDto Person { get; set; }
        [Required]
        public bool HolderStatus { get; set; }
    }
}
