using System;
using System.Collections.Generic;
using System.Text;
using TransportInfo.Domain.Entities;
using TransportInfo.Domain.Services;

namespace TransportInfo.Domain.Contracts
{
    public interface ITransportRepository : IRepositoryBase<Transport>
    {
        PagedList<Transport> GetTransports(TransportParameters transportParameters);
        Transport GetTransport(Guid transportId, bool includeDetails);
        public IEnumerable<TransportPerson> GetTransportHolderPersons(Guid transportId);
        void CreateTransportWithNewGuid(Transport transport);
        bool TransportExists(Guid transportId);

    }
}
