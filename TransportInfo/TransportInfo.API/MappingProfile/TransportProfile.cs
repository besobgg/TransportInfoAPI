using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.Domain.Entities;
using TransportInfo.API.DtoModels;
using TransportInfo.API.DtoModels.TransportDtos;

namespace TransportInfo.API.MappingProfile
{
    public class TransportProfile : Profile
    {
        public TransportProfile()
        {
            CreateMap<Transport, TransportWithoutDetailsDto>();
            CreateMap<Transport, TransportDto>();
            CreateMap<TransportForCreationDto, Transport>();
            CreateMap<Transport,CreatedTransportDto>();
            CreateMap<Transport,TransportForUpdateDto>().ReverseMap();

        }
    }
}
