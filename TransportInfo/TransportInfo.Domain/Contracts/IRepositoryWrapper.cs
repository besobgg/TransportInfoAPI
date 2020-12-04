using System;
using System.Collections.Generic;
using System.Text;
using TransportInfo.Domain.Contracts;

namespace TransportInfo.Domain.Contracts
{
    public interface IRepositoryWrapper
    {
        ITransportRepository Transport { get; }
        IPersonRepository Person { get; }
        ITransportPersonRepository TransportPerson  { get; }
        IFuelRepository Fuel { get; }
        void Save();

    }
}
