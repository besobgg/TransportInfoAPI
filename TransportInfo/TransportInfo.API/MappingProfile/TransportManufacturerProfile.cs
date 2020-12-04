using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.Domain.Entities;
using TransportInfo.API.DtoModels;
using TransportInfo.API.DtoModels.TransportManufacturerDtos;

namespace TransportInfo.API.MappingProfile
{
    public class TransportManufacturerProfile : Profile
    {
        public TransportManufacturerProfile()
        {
            CreateMap<TransportManufacturer, TransportManufacturerWitoutDetailsDto>();
        }
    }
}
