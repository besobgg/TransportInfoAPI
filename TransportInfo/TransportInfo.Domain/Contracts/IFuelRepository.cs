using System;
using System.Collections.Generic;
using System.Text;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Domain.Contracts
{
   public  interface IFuelRepository : IRepositoryBase<Fuel>
    {
        public Fuel GetFuelById(int Id);
    }
}
