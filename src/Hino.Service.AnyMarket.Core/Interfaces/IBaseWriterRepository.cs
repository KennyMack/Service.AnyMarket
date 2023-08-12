using Hino.Service.AnyMarket.Entities;
using Hino.Service.AnyMarket.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Core.Interfaces
{
    public interface IBaseWriterRepository<T> : IDisposable where T : BaseEntity
    {
        bool Add(T model);
        bool Update(T model);
        bool Remove(T model);
        Task<T> RemoveByKeyAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr);
        Task<int> SaveChangesAsync(CancellationToken cancellation);
    }
}
