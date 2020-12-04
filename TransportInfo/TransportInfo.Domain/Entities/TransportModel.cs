using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Entities
{
    public class TransportModel
    {
        public int Id { get; set; }
        public string ModelName { get; set; }
        public string ModelNameGE { get; set; }

        public int ManufacturerId { get; set; }
        public TransportManufacturer Manufacturer { get; set; }
        public ICollection<Transport> Transports { get; set; }

    }
}
