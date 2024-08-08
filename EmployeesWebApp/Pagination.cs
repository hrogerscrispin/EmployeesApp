namespace EmployeesWebApp
{
    public class Pagination
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }

        public Pagination()
        {
            
        }

        public Pagination(int totalItems, int page, int pageSize = 5)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currenPage = page;

            int startPage = Math.Max(currenPage - 5, 1);
            int endPage = Math.Min(startPage + 4, totalPages);

            if (endPage - startPage < 4)
            {
                startPage = Math.Max(endPage - 4, 1);
            }

            TotalItems = totalItems;
            CurrentPage = currenPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

    }
}
