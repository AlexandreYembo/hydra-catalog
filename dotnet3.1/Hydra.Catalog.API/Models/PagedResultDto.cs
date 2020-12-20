using System.Collections.Generic;

namespace Hydra.Catalog.API.Models
{
    public class PagedResultDto<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int TotalResult { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }

        public PagedResultDto(IEnumerable<T> list, int totalResult, int pageIndex, int pageSize, string query)
        {
            List = list;
            TotalResult = totalResult;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Query = query;
        }
    }
}