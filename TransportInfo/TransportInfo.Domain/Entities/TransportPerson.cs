using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Entities
{
    public class TransportPerson
    {
        public Guid TransportId { get; set; }
        public Transport Transport { get; set;}
        public Guid PersonId { get; set; }
        public Person Person { get; set; }
        public  bool  HolderStatus { get; set; }

    }
}
