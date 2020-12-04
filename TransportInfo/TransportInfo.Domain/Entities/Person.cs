using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Entities
{
    public class Person
    {
        public Guid PersonId { get; set; }       
        public string FirstName { get; set; }       
        public string LastName { get; set; }
        public string PersonalNumber { get; set; }
        public string Phone { get; set; }
        public ICollection<TransportPerson> TransportPersons { get; set; }
    }
}
