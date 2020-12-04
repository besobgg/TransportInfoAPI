using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using TransportInfo.API.Validators;
using TransportInfo.API.DtoModels.TransportDtos;
using TransportInfo.Domain.Services;
using TransportInfo.API.DtoModels.TransportPersonDtos;
using Microsoft.AspNetCore.Http;

namespace TransportInfo.API.Controllers
{
    [Produces("application/json")]
    [Route("api/transports")]
    [ApiController]   
    public class TransportsController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private IWebHostEnvironment _webHostEnviroment;

        public TransportsController(IStringLocalizer<PersonsController> localizer, IRepositoryWrapper repositoryWrapper, IMapper mapper, ILoggerManager logger, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repositoryWrapper ??
                throw new ArgumentNullException(nameof(repositoryWrapper));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _logger = logger ??
              throw new ArgumentNullException(nameof(logger));
            _webHostEnviroment = webHostEnvironment ??
             throw new ArgumentNullException(nameof(webHostEnvironment));
            _localizer = localizer;

        }

        //
        // transport CRUD
        //

        /// <summary>
        /// Gets The List Of All Transports With Paging and Ordering Functions
        /// </summary>
        /// <param name="transportParameters"></param>
        /// <returns>The List Of Transports</returns>
       
        [HttpGet]
        public IActionResult GetTranspors([FromQuery] TransportParameters transportParameters)
        {
            Console.WriteLine($"Current Culture:{CultureInfo.CurrentCulture }");
            Console.WriteLine($"Current  UI Culture:{CultureInfo.CurrentUICulture }");

            if (!transportParameters.ValidYearRange)
            {
                return BadRequest("Max year of manufacture cannot be less than min year of manufacture");
            }
            var transportEntities = _repository.Transport.GetTransports(transportParameters);
            var metadata = new
            {
                transportEntities.TotalCount,
                transportEntities.PageSize,
                transportEntities.CurrentPage,
                transportEntities.TotalPages,
                transportEntities.HasNext,
                transportEntities.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            _logger.LogInfo($"Returned {transportEntities.TotalCount} transports from database.");
            var transportsResult = _mapper.Map<IEnumerable<TransportWithoutDetailsDto>>(transportEntities);
            return Ok(transportsResult);

        }

        /// <summary>
        /// Gets The  Transport With Details Or Not Depends includeDetails Parameter
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="includeDetails"></param>
        /// <returns>Transport </returns>
        [HttpGet("{Id}", Name = "GetTransportById")]
        public IActionResult GetTransportById(Guid Id, bool includeDetails = true)
        {
            var transportEntities = _repository.Transport.GetTransport(Id, includeDetails);
            if (transportEntities == null)
            {
                _logger.LogError($"Transport with id: {Id}, hasn't been found in db.");
                return NotFound();
            }

            if (includeDetails)
            {
                return Ok(_mapper.Map<TransportDto>(transportEntities));
            }

            return Ok(_mapper.Map<TransportWithoutDetailsDto>(transportEntities));

        }


        /// <summary>
        /// Creates Transport With Image, ObjectFile Should Conteins Image and String Of Transport Object With Curly Brackets
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST api/transports
        ///     {  
        ///     
        ///      "vinCode": "34353453THFDHJ87Y6", 
        ///      "registrationNumber": "AA-200-AA",
        ///      "manufactureDate": "1990-04-09T00:00:00",
        ///      "photo": null,
        ///      "manufacturerId": 11,
        ///      "modelId": 11, 
        ///      "colorId": 12,
        ///      "fuelId": 11,       
        ///     }
        /// </remarks>
        /// <param name="objectFile"></param>
        /// <returns></returns> 
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedTransportDto))]
        [HttpPost]       
        public IActionResult CreateTransport([FromForm] TransportFileUpload objectFile)
        {
            TransportForCreationDto transport = JsonConvert.DeserializeObject<TransportForCreationDto>(objectFile.transport);

            string path = _webHostEnviroment.WebRootPath;

            if (transport == null)
            {
                _logger.LogError("Trransport object sent from client is null.");
                return BadRequest("Transport object is null");
            }
            var transportValidator = new TransportForCreationDtoValidator();
            var result = transportValidator.Validate(transport);

            result.AddToModelState(ModelState, null);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid transport object sent from client.");
                return BadRequest(result.ToString());
            }

            if (objectFile.image != null && objectFile.image.Length > 0 && objectFile.image.ContentType.StartsWith("image"))
            {

                if (!Directory.Exists(path))
                {
                    path = _webHostEnviroment.WebRootPath = _webHostEnviroment.ContentRootPath + "\\wwwroot";
                    Directory.CreateDirectory(path);
                }

                path = _webHostEnviroment.WebRootPath + "\\uploads\\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                path = path + Guid.NewGuid().ToString() + objectFile.image.FileName;
                using (FileStream fileStream = System.IO.File.Create(path))
                {
                    objectFile.image.CopyTo(fileStream);
                    fileStream.Flush();
                    transport.Photo = path;
                }

            }

            var transportEntity = _mapper.Map<Transport>(transport);
            _repository.Transport.CreateTransportWithNewGuid(transportEntity);
            _repository.Save();

            var createdTransport = _mapper.Map<CreatedTransportDto>(transportEntity);

            return CreatedAtRoute("GetTransportById", new { Id = createdTransport.TransportId }, createdTransport);

        }



        /// <summary>
        /// Updates Transport With Image, ObjectFile Should Conteins Image and String Of Transport Object  With Curly Brackets
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT api/transports
        ///     {  
        ///     
        ///      "vinCode": "34353453THFDHJ87Y6", 
        ///      "registrationNumber": "AA-200-AA",
        ///      "manufactureDate": "1990-04-09T00:00:00",
        ///      "photo": null,
        ///      "manufacturerId": 11,
        ///      "modelId": 11, 
        ///      "colorId": 12,
        ///      "fuelId": 11,       
        ///     }
        /// </remarks>
        /// <param name="Id"></param>
        /// <param name="objectFile"></param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]       
        public IActionResult UpdateTransportWithPhoto(Guid Id, [FromForm] TransportFileUpload objectFile, bool includeDetails = false)
        {
            TransportForUpdateDto transport = JsonConvert.DeserializeObject<TransportForUpdateDto>(objectFile.transport);

            string path = _webHostEnviroment.WebRootPath;

            if (transport == null)
            {
                _logger.LogError("Transport object sent from client is null.");
                return BadRequest("Transport object is null");
            }

            var transportEntity = _repository.Transport.GetTransport(Id, includeDetails);
            if (transportEntity == null)
            {
                _logger.LogError($"Transport with id: {Id}, hasn't been found in db.");
                return NotFound();
            }

            if (objectFile.image != null && objectFile.image.Length > 0 && objectFile.image.ContentType.StartsWith("image"))
            {

                if (!Directory.Exists(path))
                {
                    path = _webHostEnviroment.WebRootPath = _webHostEnviroment.ContentRootPath + "\\wwwroot";
                    Directory.CreateDirectory(path);
                }

                path = _webHostEnviroment.WebRootPath + "\\uploads\\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }


                path = path + Guid.NewGuid().ToString() + objectFile.image.FileName;
                using (FileStream fileStream = System.IO.File.Create(path))
                {
                    objectFile.image.CopyTo(fileStream);
                    fileStream.Flush();
                    transport.Photo = path;
                }

            }
            if (transport.Photo == null && transportEntity.Photo != "")
                transport.Photo = transportEntity.Photo;
            _mapper.Map(transport, transportEntity);

            _repository.Transport.Update(transportEntity);
            _repository.Save();

            return NoContent();

        }

        //[HttpPut("{Id}")]
        //public IActionResult UpdateTransport(Guid Id, [FromBody] TransportForUpdateDto transport, bool includeDetails = false)

        //{

        //    if (transport == null)
        //    {
        //        _logger.LogError("Transport object sent from client is null.");
        //        return BadRequest("Transport object is null");
        //    }

        //    var transportEntity = _repository.Transport.GetTransport(Id, includeDetails);
        //    if (transportEntity == null)
        //    {
        //        _logger.LogError($"Transport with id: {Id}, hasn't been found in db.");
        //        return NotFound();
        //    }

        //    _mapper.Map(transport, transportEntity);

        //    _repository.Transport.Update(transportEntity);
        //    _repository.Save();

        //    return NoContent();

        //}

        [HttpDelete("{Id}")]
        public IActionResult DeleteTransport(Guid Id, bool includeDetails = false)
        {
            var transport = _repository.Transport.GetTransport(Id, includeDetails);
            if (transport == null)
            {
                _logger.LogError($"Transport with id: {Id}, hasn't been found in db.");
                return NotFound();
            }

            _repository.Transport.Delete(transport);
            _repository.Save();

            return NoContent();

        }


    //
    // transportHolderPersons CRUD
    //

        [HttpPost("{transportId}/persons")]
        public IActionResult CreateTransportHolderPerson(Guid transportId, [FromBody] TransportPersonForCreationDto transportPerson)
        {
            if (_repository.Transport.TransportExists(transportId))
            {
                if (transportPerson == null)
                {
                    _logger.LogError("TransportPerson object sent from client is null.");
                    return BadRequest("TransportPerson object is null");
                }


                var transportPersonEntity = _mapper.Map<TransportPerson>(transportPerson);
                if (_repository.Person.Exists(transportPersonEntity.Person))
                {

                    transportPersonEntity.Person = _repository.Person.GetPerson(transportPersonEntity.Person.PersonId, false);
                }
                else
                {
                    _repository.Person.CreatePersonWithNewGuid(transportPersonEntity.Person);
                    _repository.Save();
                }


                var tempTransportPerson = new TransportPerson();
                tempTransportPerson.TransportId = transportId;
                tempTransportPerson.PersonId = transportPersonEntity.Person.PersonId;
                tempTransportPerson.HolderStatus = transportPersonEntity.HolderStatus;


                //
                // exist holder status change 
                //

                if (_repository.TransportPerson.Exists(tempTransportPerson))
                {
                    var existTransportPerson = _repository.TransportPerson.GetTransportPerson(transportId,tempTransportPerson.PersonId);
                    if(existTransportPerson.HolderStatus==true)
                    {
                        _logger.LogError($"TransportPerson have been found in db and is Already Holder .");
                        return BadRequest($"TransportPerson have been found in db and is Already Holder .");
                    }
                    else
                    {
                        if (tempTransportPerson.HolderStatus == false)
                        {
                            _logger.LogError($"TransportPerson have been found in db.");
                            return BadRequest($"TransportPerson have been found in db.");
                        }
                        else
                        {
                            existTransportPerson.HolderStatus = true;
                            _repository.TransportPerson.Update(existTransportPerson);
                            _repository.Save();
                            return NoContent();
                        }
                    }

                }

                //
                // new holder make another holders as old holders 
                //

                if (transportPersonEntity.HolderStatus)
                {
                    var transport = _repository.Transport.GetTransport(transportId, true);
                    foreach (var item in transport.TransportPersons)
                    {
                        if (item.HolderStatus)
                        {
                            item.HolderStatus = false;
                            _repository.TransportPerson.Update(item);
                        }

                    }

                }

                _repository.TransportPerson.Create(tempTransportPerson);
                _repository.Save();

                return NoContent();

               
            }
            else
            {
                _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
                return NotFound($"Transport with id: {transportId}, hasn't been found in db.");

            }
        }

        [HttpGet("{Id}/persons", Name = "GetTransporHolderPersons")]
        public IActionResult GetTransportHolderPersons(Guid Id)
        {

            if (!_repository.Transport.TransportExists(Id))
            {
                _logger.LogError($"Transport with id: {Id}, hasn't been found in db.");
                return NotFound();
            }
            var transportPersonEntities = _repository.Transport.GetTransportHolderPersons(Id);


            return Ok(_mapper.Map<IEnumerable<TransportPersonWithoutDetailsDto>>(transportPersonEntities));

        }
        /// <summary>
        /// The Delete Method Make Transport Holder As an Old Holder
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>

        [HttpDelete("{transportId}/persons/{personId}")]
        public IActionResult DeleteTransportHolderPerson(Guid transportId, Guid personId)
        {

            if (_repository.Transport.TransportExists(transportId))
            {
                var tperson = _repository.TransportPerson.GetTransportHolderPersonById(transportId, personId);
                if (tperson == null)
                {
                    _logger.LogError($"TransportPerson with id: {personId}, hasn't been found in db.");
                    return BadRequest("TransportPerson object is null");
                }

                tperson.HolderStatus = false;
                _repository.TransportPerson.Update(tperson);
                _repository.Save();

                return NoContent();
            }
            else
            {
                _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
                return NotFound($"Transport with id: {transportId}, hasn't been found in db.");

            }

        }




    //
    // transport image CRUD
    //


        [HttpPost("{transportId}/image")]
        public IActionResult UploadTransportImage([FromForm] TransportPhotoUpload objectFile, Guid transportId, bool includeDetails = false)
        {
            if (objectFile.image.Length > 0 && objectFile.image.ContentType.StartsWith("image"))
            {
                var transport = _repository.Transport.GetTransport(transportId, includeDetails);
                if (transport == null)
                {
                    _logger.LogError($"Transpor with id: {transportId}, hasn't been found in db.");
                    return NotFound("Transpor  Not Found");
                }


                string path = _webHostEnviroment.WebRootPath;
                if (!Directory.Exists(path))
                {
                    path = _webHostEnviroment.WebRootPath = _webHostEnviroment.ContentRootPath + "\\wwwroot";
                    Directory.CreateDirectory(path);
                }

                path = _webHostEnviroment.WebRootPath + "\\uploads\\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = path + Guid.NewGuid().ToString() + objectFile.image.FileName;
                using (FileStream fileStream = System.IO.File.Create(path))
                {
                    objectFile.image.CopyTo(fileStream);
                    fileStream.Flush();
                    transport.Photo = path;
                    _repository.Transport.Update(transport);
                    _repository.Save();

                    return NoContent();
                }

            }
            else
            {
                return BadRequest("No Upload image file ");
            }

        }

        [HttpPut("{transportId}/image")]
        public IActionResult UpdateTransportImage([FromForm] TransportPhotoUpload objectFile, Guid transportId, bool includeDetails = false)
        {
            if (objectFile.image.Length > 0 && objectFile.image.ContentType.StartsWith("image"))
            {
                var transport = _repository.Transport.GetTransport(transportId, includeDetails);
                if (transport == null)
                {
                    _logger.LogError($"Transport with id: {transportId}, hasn't been found in db.");
                    return NotFound("Transport Not Found");
                }


                string path = _webHostEnviroment.WebRootPath;
                if (!Directory.Exists(path))
                {
                    path = _webHostEnviroment.WebRootPath = _webHostEnviroment.ContentRootPath + "\\wwwroot";
                    Directory.CreateDirectory(path);
                }

                path = _webHostEnviroment.WebRootPath + "\\uploads\\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                path = path + Guid.NewGuid().ToString() + objectFile.image.FileName;
                using (FileStream fileStream = System.IO.File.Create(path))
                {
                    if (System.IO.File.Exists(transport.Photo))
                    {
                        System.IO.File.Delete(transport.Photo);
                    }

                    objectFile.image.CopyTo(fileStream);
                    fileStream.Flush();

                    transport.Photo = path;
                    _repository.Transport.Update(transport);
                    _repository.Save();

                    return NoContent();
                }

            }
            else
            {
                return BadRequest("No Upload image file ");
            }

        }

        [HttpDelete("{transportId}/image")]
        public IActionResult DeleteTransportImage(Guid transportId, bool includeDetails = false)
        {
            var transport = _repository.Transport.GetTransport(transportId, includeDetails);
            if (transport == null || transport.Photo == "")
            {
                _logger.LogError($"Transport with id: {transportId}, hasn't been found in db Or Transport Picture Not Found.");
                return NotFound("Transport  Or Transport Picture Not Found");
            }

            string path = transport.Photo;

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(transport.Photo);
            }

            transport.Photo = "";
            _repository.Transport.Update(transport);
            _repository.Save();
            return NoContent();

        }


    }
}
