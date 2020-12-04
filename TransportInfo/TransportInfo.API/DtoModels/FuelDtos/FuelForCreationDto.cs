using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportInfo.API.DtoModels.FuelDtos
{
    public class FuelForCreationDto
    {
        [Required]
        public string FuelTypeGE { get; set; }
        [Required]
        public string FuelType { get; set; }
    }
}
