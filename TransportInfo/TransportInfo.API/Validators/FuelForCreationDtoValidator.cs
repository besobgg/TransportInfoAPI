using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

using TransportInfo.API.DtoModels.FuelDtos;

namespace TransportInfo.API.Validators
{
    public class FuelForCreationDtoValidator : AbstractValidator<FuelForCreationDto>
    {
        public FuelForCreationDtoValidator()
    {
        RuleFor(x => x.FuelType).NotEmpty();
        RuleFor(x => x.FuelTypeGE).NotEmpty();
    }
}
}
