using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportInfo.API.DtoModels.TransportDtos
{
    public class TransportForCreationDto
    {

        [Required]
        [StringLength(20, MinimumLength = 7)]
        public string VinCode { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string RegistrationNumber { get; set; }
        [Required]
        public DateTime ManufactureDate { get; set; }
        public string Photo { get; set; }
        [Required]
        public int ManufacturerId { get; set; }
        [Required]
        public int ModelId { get; set; }
        [Required]
        public int ColorId { get; set; }
        [Required]
        public int FuelId { get; set; }
        public string Note { get; set; }

    }
}
