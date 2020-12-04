using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.API.DtoModels.ColorDtos;
using TransportInfo.API.DtoModels.FuelDtos;
using TransportInfo.API.DtoModels.TransportManufacturerDtos;
using TransportInfo.API.DtoModels.TransportModelDtos;
using TransportInfo.API.DtoModels.TransportPersonDtos;

namespace TransportInfo.API.DtoModels.TransportDtos
{
    public class TransportDto
    {
        public Guid TransportId { get; set; }
        public string VinCode { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Photo { get; set; }       
        public int ManufacturerId { get; set; }
        public TransportManufacturerWitoutDetailsDto Manufacturer { get; set; }
        public int ModelId { get; set; }
        public TransportModelWitoutDetailsDto Model { get; set; }
        public int ColorId { get; set; }
        public ColorWithoutDetailsDto Color { get; set; }
        public int FuelId { get; set; }
        public FuelWithutDetailsDto Fuel { get; set; }
        public ICollection<TransportPersonWithoutDetailsDto> TransportPersons { get; set; }
        public string Note { get; set; }
    }
}
