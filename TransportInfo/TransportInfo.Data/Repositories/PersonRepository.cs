using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TransportInfo.Data.Contexts;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;

namespace TransportInfo.Data.Repositories
{
    public class PersonRepository : RepositoryBase<Person>, IPersonRepository
    {
        
        public PersonRepository(TransportInfoContext transportInfoContext)
            : base(transportInfoContext)
        {
            
        }

        public void CreatePersonWithNewGuid(Person person)
        {
            person.PersonId = Guid.NewGuid();
            Create(person);
        }

        public Person GetPerson(Guid personId, bool includeDetails)
        {
            if (includeDetails)
            {
                return FindByCondition(person => person.PersonId.Equals(personId))
                    .Include(tp=>tp.TransportPersons)
                    .ThenInclude(t => t.Transport)
                    .FirstOrDefault();
            }
            return FindByCondition(person => person.PersonId.Equals(personId))
                   .FirstOrDefault();
        }

        public bool PersonExists(Guid personId)
        {
            return FindByCondition(person => person.PersonId.Equals(personId)).Any();
        }
    }
}
