using System.Collections.Generic;

namespace Hydra.Catalog.Domain
{
    /// <summary>
    /// Pagination class
    /// </summary>
    public class PagedResult<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int TotalResult { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
    }
}