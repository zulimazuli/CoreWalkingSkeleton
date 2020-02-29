using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTemplate.ApplicationCore.Specifications
{
    public interface IQueryParameters<T>
    {
        bool IsPagingEnabled { get; }
        int PageNumber { get; }
        int PageSize { get; }
    }
}
