using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.Domain.Entities;
using TransportInfo.API.DtoModels;
using TransportInfo.API.DtoModels.TransportPersonDtos;

namespace TransportInfo.API.MappingProfile
{
    public class TransportPersonProfile : Profile
    {
        public TransportPersonProfile()
        {
            CreateMap<TransportPerson, TransportPersonWithoutDetailsDto>();
            CreateMap<TransportPersonForCreationDto ,TransportPerson>();
            CreateMap<TransportPerson, TransportPersonWithOnlyTransportDto>(); 
        }
    }
}
