using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TransportInfo.API.DtoModels;
using TransportInfo.API.DtoModels.PersonDtos;

namespace TransportInfo.API.Validators
{
    public class PersonForCreationDtoValidator: AbstractValidator<PersonForCreationDto>
    {
        public PersonForCreationDtoValidator()
        {
              RuleFor(x => x.FirstName).NotEmpty().MaximumLength(15);
              RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);            
              RuleFor(x => x.PersonalNumber).NotEmpty().MinimumLength(8).MaximumLength(11);
            
        }
    }
}
