using System.Collections.Generic;

namespace Domain.Dto
{
    public class PaginacaoDto<T>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public int TotalRecords { get; init; }
        public IEnumerable<T> Data { get; init; }
    }
}
