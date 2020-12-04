using System.Collections.Generic;
using System.Linq;
using TransportInfo.Data.Contexts;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Repositories
{
    public  class FuelRepository : RepositoryBase<Fuel>, IFuelRepository
    {        

        public FuelRepository(TransportInfoContext transportInfoContext)
            : base(transportInfoContext)
        {
            
        }
        public Fuel GetFuelById(int Id)
        {
            var fuel = FindByCondition(fuel => fuel.Id.Equals(Id))
                 .FirstOrDefault();

            return fuel;
        }

    }
}