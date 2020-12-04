using System;
using System.Collections.Generic;
using System.Text;
using TransportInfo.Data.Contexts;
using TransportInfo.Domain.Contracts;
using TransportInfo.Domain.Entities;


namespace TransportInfo.Data.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private TransportInfoContext _contex;
        private PersonRepository _person;
        private TransportRepository _transport;
        private TransportPersonRepository _transportPerson;
        private FuelRepository _fuel;

        private ISortHelper<Transport> _transportSortHelper;
        
        public ITransportRepository Transport 
        {
            get 
            {
                if (_transport == null)
                {
                    _transport = new TransportRepository(_contex, _transportSortHelper);
                }
                return _transport;

            }
        }

        public IPersonRepository Person 
        {
            get 
            {
                if (_person == null)
                {
                    _person = new PersonRepository(_contex);
                }
                return _person;

            }
        }
        public ITransportPersonRepository TransportPerson
        {
            get
            {
                if (_transportPerson == null)
                {
                    _transportPerson = new TransportPersonRepository(_contex);
                }
                return _transportPerson;

            }
        }

        public IFuelRepository Fuel
        {
            get
            {
                if (_fuel == null)
                {
                    _fuel = new FuelRepository(_contex);
                }
                return _fuel;

            }
        }


        public RepositoryWrapper (TransportInfoContext transportInfoContext, ISortHelper<Transport> transportSortHelper)
        {
            _contex = transportInfoContext;
            _transportSortHelper = transportSortHelper;
        }
        public void Save()
        {
            _contex.SaveChanges();
        }
    }
}
