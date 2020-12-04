using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportInfo.Domain.Contracts
{
    public interface ISortHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
    }
}
