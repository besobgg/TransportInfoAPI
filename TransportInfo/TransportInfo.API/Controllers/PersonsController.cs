using System;
using System.Collections.Generic;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransportInfo.API.DtoModels.PersonDtos;
using TransportInfo.API.Validators;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;

namespace TransportInfo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public PersonsController(IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerManager logger)
        {
            _repository = repositoryWrapper ??
                throw new ArgumentNullException(nameof(repositoryWrapper));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
              throw new ArgumentNullException(nameof(logger));
            
        }

        [HttpGet()]
        public IActionResult GetPersons()
        {
            var personEntities = _repository.Person.GetAll();
            return Ok(_mapper.Map<IEnumerable<PersonWithoutDetailsDto>>(personEntities));
        }

        [HttpGet("{Id}", Name = "GetPersonById")]
        public IActionResult GetPersonById(Guid Id, bool includeDetails = true)
        {
            var personEntities = _repository.Person.GetPerson(Id, includeDetails);
            if (personEntities == null)
            {
                _logger.LogError($"Person with id: {Id}, hasn't been found in db.");
                return NotFound();
            }

            if (includeDetails)
            {
                return Ok(_mapper.Map<PersonDto>(personEntities));
            }

            return Ok(_mapper.Map<PersonWithoutDetailsDto>(personEntities));

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PersonWithoutDetailsDto))]
        public IActionResult CreatePerson([FromBody] PersonForCreationDto person)
        {
            
            if (person == null)
            {
                _logger.LogError("person object sent from client is null.");
                return BadRequest("person object is null");
            }
            var personValidator = new PersonForCreationDtoValidator();
            var result = personValidator.Validate(person);

            result.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid person object sent from client.");
                return BadRequest(result.ToString());
            }

            var personEntity = _mapper.Map<Person>(person);
            _repository.Person.CreatePersonWithNewGuid(personEntity);
            _repository.Save();



            var createdPerson = _mapper.Map<PersonWithoutDetailsDto>(personEntity);

            return CreatedAtRoute("GetPersonById", new { id = createdPerson.PersonId }, createdPerson);

        }
        [HttpDelete("{Id}")]
        public IActionResult DeletePerson(Guid Id, bool includeDetails = false)
        {
            var person = _repository.Person.GetPerson(Id, includeDetails);
            if (person == null)
            {
                _logger.LogError($"Person with id: {Id}, hasn't been found in db.");
                return NotFound();
            }
            

            _repository.Person.Delete(person);
            _repository.Save();

            return NoContent();

        }

    }
}
