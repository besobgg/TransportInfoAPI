using System;
using System.Collections.Generic;
using System.Text;
using TransportInfo.Domain.Entities;
namespace TransportInfo.Domain.Contracts
{
   public  interface IPersonRepository  : IRepositoryBase<Person>
    {
        void CreatePersonWithNewGuid(Person person);
        bool PersonExists(Guid personId);
        Person GetPerson(Guid personId, bool includeDetails);
    }
}
