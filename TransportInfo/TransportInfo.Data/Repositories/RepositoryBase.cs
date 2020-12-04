using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using TransportInfo.Data.Contexts;
using TransportInfo.Domain.Contracts;

namespace TransportInfo.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private TransportInfoContext _contex { get; set; }
        public RepositoryBase (TransportInfoContext transportInfoContext)
        {
            _contex = transportInfoContext 
                ?? throw new ArgumentNullException(nameof(transportInfoContext));
        }

        public IQueryable<T> GetAll()
        {
            return _contex.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _contex.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _contex.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _contex.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _contex.Set<T>().Update(entity);
        }

        public T GetById(Guid id)
        {
            return _contex.Set<T>().Find(id);
        }

        public bool Exists(T entity)
        {
            return _contex.Set<T>().Contains(entity);
        }
        
    }
}
