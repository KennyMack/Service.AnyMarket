using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Utils.Paging
{
    public class PagedResult<T> : BasePagedResult where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }

        public PagedResult(List<T> pItems)
        {
            Results = pItems;
            CurrentPage = 1;
            PageCount = 1;
            var CountItems = pItems.Count;
            PageSize = CountItems;
            RowCount = CountItems;
            HasNext = false;
        }
    }
}
