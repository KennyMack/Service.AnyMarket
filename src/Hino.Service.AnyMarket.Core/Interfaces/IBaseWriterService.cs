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
    public interface IBaseWriterService<T> : IDisposable, IErrorBaseService where T : BaseEntity
    {
        T Add(T model);
        T Update(T model);
        T Remove(T model);
        Task<T> RemoveByKeyAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr);
        Task<int> SaveChangesAsync(CancellationToken cancellation);
    }
}
