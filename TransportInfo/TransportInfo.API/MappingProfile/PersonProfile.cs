using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.Domain.Entities;
using TransportInfo.API.DtoModels;
using TransportInfo.API.DtoModels.PersonDtos;

namespace TransportInfo.API.MappingProfile
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonWithoutDetailsDto>();
            CreateMap<PersonCreationForTransportDto, Person>();
            CreateMap<Person, PersonDto>();
            CreateMap<PersonForCreationDto,Person>();
        }
    }
}
