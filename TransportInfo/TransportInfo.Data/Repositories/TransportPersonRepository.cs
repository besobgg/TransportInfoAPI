using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TransportInfo.Data.Contexts;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;


namespace TransportInfo.Data.Repositories
{
    public class TransportPersonRepository : RepositoryBase<TransportPerson>, ITransportPersonRepository
    {
        public TransportPersonRepository(TransportInfoContext transportInfoContext)
            : base(transportInfoContext)
        {
           
        }

        public TransportPerson GetTransportPerson(Guid transportId, Guid personId)
        {
            return FindByCondition(tp => tp.TransportId.Equals(transportId) && tp.PersonId.Equals(personId))
                            .Include(p => p.Person)
                            .FirstOrDefault();
        }
        public TransportPerson GetTransportHolderPersonById(Guid transportId, Guid personId)
        {
            return FindByCondition(tp => tp.TransportId.Equals(transportId) && tp.PersonId.Equals(personId))
                .Include(p => p.Person)
                .FirstOrDefault();
        }

        public IEnumerable<TransportPerson> GetAllTransportPersons(Guid transportId)
        {

            return FindByCondition(tp => tp.TransportId.Equals(transportId))
                .Include(p => p.Person)
                .ToList();
        }
    }
}
