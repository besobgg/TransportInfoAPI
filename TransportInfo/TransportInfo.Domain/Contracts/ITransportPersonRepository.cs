using System;
using System.Collections.Generic;
using System.Text;
using TransportInfo.Domain.Entities;
namespace TransportInfo.Domain.Contracts
{
    public interface ITransportPersonRepository : IRepositoryBase<TransportPerson>
    {
        IEnumerable<TransportPerson> GetAllTransportPersons(Guid transportId);
        public TransportPerson GetTransportHolderPersonById(Guid transportId, Guid personId);
        public TransportPerson GetTransportPerson(Guid transportId, Guid personId);


    }
}
