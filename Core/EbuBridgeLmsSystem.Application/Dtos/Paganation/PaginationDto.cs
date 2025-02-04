namespace EbuBridgeLmsSystem.Application.Dtos.Paganation
{
    public class PaginationDto<T>
    {
        public PaginationDto(List<T> items, int currentPage, int totalPages, int totalRecords, int pageSize)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            PageSize = pageSize;
        }

        public List<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }

        public bool HasNext => CurrentPage < TotalPages;
        public bool HasPrev => CurrentPage > 1;



    }

}