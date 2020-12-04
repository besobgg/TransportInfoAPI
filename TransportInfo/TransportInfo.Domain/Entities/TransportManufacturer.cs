using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Entities
{
    public class TransportManufacturer
    {
        public int Id { get; set; }

        public string ManufacturerName { get; set; }
        public string ManufacturerNameGE { get; set; }
        public ICollection<TransportModel> TransportModels { get; set; }
    }
}
