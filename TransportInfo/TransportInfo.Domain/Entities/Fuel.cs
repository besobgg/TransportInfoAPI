using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Entities
{
    public class Fuel
    {
        public int Id { get; set; }

        public string FuelTypeGE { get; set; }
        public string FuelType { get; set; }

        public ICollection<Transport> Transports { get; set; }
    }
}
