using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Services
{
   public  class TransportParameters : QueryStringParameters
    {
        public TransportParameters()
        {
            OrderBy = "ManufactureDate";
        }
        public uint MinYearOfManufacture { get; set; }
        public uint MaxYearOfManufacture { get; set; } = (uint)DateTime.Now.Year;
        public bool ValidYearRange => MaxYearOfManufacture > MinYearOfManufacture;
        public string VinCode { get; set; }        
        public string RegistrationNumber { get; set; }

    }
}
