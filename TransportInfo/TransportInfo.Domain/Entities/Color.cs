using System;
using System.Collections.Generic;
using System.Text;

namespace TransportInfo.Domain.Entities
{
   public class Color
    {
        public int  Id { get; set; }
        public string ColorNameGE { get; set; }
        public string ColorName { get; set; }
        public ICollection<Transport> Transports { get; set; }
    }
}
