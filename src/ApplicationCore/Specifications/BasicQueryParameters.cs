namespace CoreTemplate.ApplicationCore.Specifications
{
    public class BasicQueryParameters<T> : IQueryParameters<T>
    {


        public bool IsPagingEnabled { get; private set; } = false;
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public BasicQueryParameters()
        {
            
        }

        public void ApplyPaging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            IsPagingEnabled = true;
        }
    }
}