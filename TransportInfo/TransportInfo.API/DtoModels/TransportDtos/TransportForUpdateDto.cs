using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportInfo.API.DtoModels.TransportDtos
{
    public class TransportForUpdateDto
    {
        public string VinCode { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Photo { get; set; }
        public int ManufacturerId { get; set; }       
        public int ModelId { get; set; }        
        public int ColorId { get; set; }        
        public int FuelId { get; set; }
        public string Note { get; set; }
    }
}
