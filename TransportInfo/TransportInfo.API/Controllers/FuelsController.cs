using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportInfo.API.DtoModels.FuelDtos;
using TransportInfo.API.Validators;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;

namespace TransportInfo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/fuels")]
    [ApiController]
    
    public class FuelsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public FuelsController(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerManager logger)
        {
            _repository = repositoryWrapper ??
                throw new ArgumentNullException(nameof(repositoryWrapper));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
              throw new ArgumentNullException(nameof(logger));

        }

        [HttpGet()]
        public IActionResult GetFuels()
        {
            var fuelEntities = _repository.Fuel.GetAll();
            return Ok(_mapper.Map<IEnumerable<FuelDto>>(fuelEntities));
        }

        [HttpGet("{Id}", Name = "GetFuelById")]
        public IActionResult GetPersonById(int Id)
        {
            var fuelEntities = _repository.Fuel.GetFuelById(Id);
            if (fuelEntities == null)
            {
                _logger.LogError($"Fuel with id: {Id}, hasn't been found in db.");
                return NotFound();
            }
                     

            return Ok(_mapper.Map<FuelForCreationDto>(fuelEntities));

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FuelDto))]
        public IActionResult CreateFuel([FromBody] FuelForCreationDto fuel)
        {

            if (fuel == null)
            {
                _logger.LogError("fuel object sent from client is null.");
                return BadRequest("fuel object is null");
            }
            var fuelValidator = new FuelForCreationDtoValidator();
            var result = fuelValidator.Validate(fuel);

            result.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid fuel object sent from client.");
                return BadRequest(result.ToString());
            }

            var fuelEntity = _mapper.Map<Fuel>(fuel);
            _repository.Fuel.Create(fuelEntity);
            _repository.Save();



            var createdFuel = _mapper.Map<FuelDto>(fuelEntity);

            return CreatedAtRoute("GetFuelById", new { id = createdFuel.Id }, createdFuel);

        }
        [HttpDelete("{Id}")]
       
        public IActionResult DeleteFuel(int Id)
        {
            var fuel = _repository.Fuel.GetFuelById(Id);
            if (fuel == null)
            {
                _logger.LogError($"Fuel with id: {Id}, hasn't been found in db.");
                return NotFound();
            }


            _repository.Fuel.Delete(fuel);
            _repository.Save();

            return NoContent();

        }

    }

}

