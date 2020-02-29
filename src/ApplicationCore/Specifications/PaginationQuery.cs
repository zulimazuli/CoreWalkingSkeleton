using System;
using System.Collections.Generic;
using System.Text;

namespace CoreTemplate.ApplicationCore.Specifications
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        
        public PaginationQuery()
        {
            PageNumber = 1;
            PageSize = 20;
        }
        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize > 100 ? 100 : pageSize;
        }
    }
    
    public class Paginated<TModel>
    {
        public Paginated()
        {
            PageNumber = 1;
            PageSize = 20;
        }

        public IEnumerable<TModel> Model { get; set; }
        public int Count { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowPrevious => PageNumber > 1;
        public bool ShowNext => PageNumber < TotalPages;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
