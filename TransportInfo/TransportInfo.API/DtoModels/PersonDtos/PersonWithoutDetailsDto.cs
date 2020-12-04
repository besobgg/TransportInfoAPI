using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransportInfo.API.DtoModels.PersonDtos
{
    public class PersonWithoutDetailsDto
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Phone { get; set; }
        
    }
}
