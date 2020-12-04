using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TransportInfo.Data.Contexts;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;
using TransportInfo.Domain.Services;

namespace TransportInfo.Data.Repositories
{
   public  class TransportRepository : RepositoryBase<Transport>, ITransportRepository
    {
        private ISortHelper<Transport> _sortHelper;
        public TransportRepository(TransportInfoContext transportInfoContext, ISortHelper<Transport> sortHelper)
            : base(transportInfoContext)
        {
            _sortHelper = sortHelper;
        }

        public void CreateTransportWithNewGuid(Transport transport)
        {
            transport.TransportId = Guid.NewGuid();
            Create(transport);
        }

        public   Transport GetTransport(Guid transportId, bool includeDetails)
        {

            if (includeDetails)
            {
                return FindByCondition(Transport => Transport.TransportId.Equals(transportId))                    
                    .Include(c => c.Color)
                    .Include(f=> f.Fuel)
                    .Include(m=> m.Manufacturer)
                    .Include(m => m.Model)
                    .Include(rp => rp.TransportPersons)
                    .ThenInclude(y => y.Person)
                    .FirstOrDefault();
            }
            return FindByCondition(Transport => Transport.TransportId.Equals(transportId))
                   .FirstOrDefault();
        }

        public IEnumerable<TransportPerson> GetTransportHolderPersons(Guid transportId)
        {
            var transport = FindByCondition(Transport => Transport.TransportId.Equals(transportId))
                .Include(t => t.TransportPersons)
                .ThenInclude(p => p.Person)
                .FirstOrDefault();

            return transport.TransportPersons;
        }


        public PagedList<Transport> GetTransports(TransportParameters transportParameters)
        {
            var transports = FindByCondition(t => t.ManufactureDate.Year >= transportParameters.MinYearOfManufacture &&
                               t.ManufactureDate.Year <= transportParameters.MaxYearOfManufacture
                               &&  t.ModelId==t.Model.Id);              
                    

            SearchByRegistrationNumber(ref transports, transportParameters.RegistrationNumber);
            SearchByVinCode(ref transports, transportParameters.VinCode);
            
            var sortedTransports = _sortHelper.ApplySort(transports, transportParameters.OrderBy);

           
              return PagedList<Transport>.ToPagedList(sortedTransports
                    .Include(c => c.Color)
                    .Include(f => f.Fuel)
                    .Include(m => m.Manufacturer)
                    .Include(m => m.Model)
                    .Include(tp => tp.TransportPersons)
                    .ThenInclude(p => p.Person),


                transportParameters.PageNumber,
                transportParameters.PageSize); 
        }

        private void SearchByRegistrationNumber(ref IQueryable<Transport> transports, string registrationNumber)
        {
            if (!transports.Any() || string.IsNullOrWhiteSpace(registrationNumber))
                return;
            transports = transports.Where(t => t.RegistrationNumber.ToLower().Contains(registrationNumber.Trim().ToLower()));
        }
        private void SearchByVinCode(ref IQueryable<Transport> transports, string vinCode)
        {
            if (!transports.Any() || string.IsNullOrWhiteSpace(vinCode))
                return;
            transports = transports.Where(t => t.VinCode.ToLower().Contains(vinCode.Trim().ToLower()));
        }
        public bool TransportExists(Guid transportId)
        {
            return FindByCondition(Transport => Transport.TransportId.Equals(transportId)).Any();
        }
    }
}
