using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransportInfo.API.DtoModels.TransportDtos;

namespace TransportInfo.API.Validators
{
    public class TransportForCreationDtoValidator : AbstractValidator<TransportForCreationDto>
    {
        public TransportForCreationDtoValidator()
        {
            RuleFor(t => t.RegistrationNumber).NotEmpty().MinimumLength(5).MaximumLength(10);
            RuleFor(t => t.ManufactureDate).NotEmpty().LessThan(DateTime.Now);
            RuleFor(t => t.VinCode).NotEmpty().MinimumLength(7);
            RuleFor(t => t.ManufacturerId).NotEmpty();
            RuleFor(t => t.ModelId).NotEmpty();
            RuleFor(t => t.ColorId).NotEmpty();
            RuleFor(t => t.FuelId).NotEmpty();

        }
    }
}
