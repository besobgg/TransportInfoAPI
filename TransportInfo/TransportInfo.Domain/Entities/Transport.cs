using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Entities
{
   public  class Transport
    {
        public Guid TransportId { get; set; }
        public string VinCode { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Photo { get; set; }        
        public int ManufacturerId { get; set; }
        public TransportManufacturer Manufacturer { get; set; }
        public int ModelId { get; set; }
        public TransportModel Model { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int FuelId { get; set; }
        public Fuel Fuel { get; set; }
        public ICollection<TransportPerson> TransportPersons { get; set; }
        public string Note { get; set; }

    }
}
