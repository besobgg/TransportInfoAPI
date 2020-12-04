using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.Domain.Entities;
using TransportInfo.API.DtoModels;
using TransportInfo.API.DtoModels.FuelDtos;

namespace TransportInfo.API.MappingProfile
{
    public class FuelProfile : Profile
    {
        public FuelProfile()
        {
            CreateMap<Fuel, FuelWithutDetailsDto>();
            CreateMap<Fuel, FuelDto>();
            CreateMap<FuelForCreationDto,Fuel>().ReverseMap();
        }

    }
   
}
