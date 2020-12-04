using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.API.DtoModels.ColorDtos;
using TransportInfo.API.DtoModels.FuelDtos;
using TransportInfo.API.DtoModels.TransportManufacturerDtos;
using TransportInfo.API.DtoModels.TransportModelDtos;

namespace TransportInfo.API.DtoModels.TransportDtos
{
    public class TransportWithoutDetailsDto
    {
        public Guid TransportId { get; set; }
        public TransportManufacturerWitoutDetailsDto Manufacturer { get; set; }   
        public TransportModelWitoutDetailsDto Model { get; set; }
        public string VinCode { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Photo { get; set; }
        public FuelWithutDetailsDto Fuel { get; set; }
        public ColorWithoutDetailsDto Color { get; set; }
        public string Note { get; set; }


    }
}
