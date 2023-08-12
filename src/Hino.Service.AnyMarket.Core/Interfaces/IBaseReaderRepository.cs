using Hino.Service.AnyMarket.Entities;
using Hino.Service.AnyMarket.Utils.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Core.Interfaces
{
    public interface IBaseReaderRepository<T> : IDisposable where T : BaseEntity
    {
        IOrderedQueryable<T> QuerySorted(IQueryable<T> source);
        Task<PagedResult<T>> GetAllPagedAsync(CancellationToken cancellation, int page, int pageSize, params Expression<Func<T, object>>[] includeProperties);
        Task<PagedResult<T>> GetAllPagedFilteredAsync(CancellationToken cancellation, int page, int pageSize, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByKeyAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> QueryAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> QueryAsync(CancellationToken cancellation, bool tracking, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties);
        Task<T> FirstOrDefaultAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties);
    }
}
