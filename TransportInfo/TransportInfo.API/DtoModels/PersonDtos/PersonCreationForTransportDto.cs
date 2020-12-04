using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransportInfo.API.DtoModels.PersonDtos
{
    public class PersonCreationForTransportDto
    {
        public Guid PersonId { get; set; }
        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }
        [Required]
        [StringLength(11, MinimumLength = 8)]
        public string PersonalNumber { get; set; }
        public string Phone { get; set; }
    }
}
