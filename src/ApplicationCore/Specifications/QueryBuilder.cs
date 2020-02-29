using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreTemplate.ApplicationCore.Entities;

namespace CoreTemplate.ApplicationCore.Specifications
{
    public class QueryBuilder<T> where T : AuditableEntity
    {
        public static IQueryable<T> Build(IQueryable<T> inputQuery, IQueryParameters<T> parameters)
        {
            var query = inputQuery;

            if (parameters.IsPagingEnabled)
            {
                query = query.Skip(parameters.PageSize * (parameters.PageNumber - 1))
                             .Take(parameters.PageSize);
            }

            return query;
        }
    }
}
