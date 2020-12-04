using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.Domain.Entities;
using TransportInfo.API.DtoModels;
using TransportInfo.API.DtoModels.ColorDtos;

namespace TransportInfo.API.MappingProfile
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, ColorWithoutDetailsDto>();
        }
        
    }
}
