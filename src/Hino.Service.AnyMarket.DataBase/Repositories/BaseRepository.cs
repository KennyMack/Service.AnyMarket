using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.DataBase.ContextDB;
using Hino.Service.AnyMarket.Entities;
using Hino.Service.AnyMarket.Utils.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.DataBase.Repositories
{
    public class BaseRepository<T> :
        IBaseReaderRepository<T>,
        IBaseWriterRepository<T>
        where T : BaseEntity
    {
        protected readonly ServiceContext DbConn;
        protected readonly DbSet<T> DbEntity;

        public BaseRepository(ServiceContext context)
        {
            DbConn = context;
            DbEntity = DbConn.Set<T>();
        }

        public virtual bool Add(T model)
        {
            DbEntity.Add(model);
            return true;
        }

        public virtual bool Update(T model)
        {
            DbConn.Entry(model).State = EntityState.Modified;

            return true;
        }

        public virtual bool Remove(T model)
        {
            DbEntity.Remove(model);
            return true;
        }

        public virtual async Task<T> RemoveByKeyAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr)
        {
            var model = await DbEntity.FirstOrDefaultAsync(expr);
            if (model == null)
                return null;

            Remove(model);
            return model;
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellation) =>
            await DbConn.SaveChangesAsync(cancellation);


        public virtual IOrderedQueryable<T> QuerySorted(IQueryable<T> source) =>
            source.OrderByDescending(r => r);

        protected async Task<PagedResult<T>> PaginateQuery(IQueryable<T> query, int page, int pageSize, CancellationToken cancellation)
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                HasNext = false
            };

            if (page > -1 && pageSize > 0)
            {
                var skip = (page - 1) * pageSize;
                var pageTotal = pageSize + 2;
                var preResult = await QuerySorted(query)
                    .Skip(skip)
                    .Take(pageTotal)
                    .ToListAsync(cancellation);


                result.HasNext = preResult.Count > pageSize;
                if (result.HasNext)
                    preResult.RemoveAt(preResult.Count - 1);

                result.Results = preResult;
            }
            else
            {
                result.Results = await query.ToListAsync(cancellation);
                result.CurrentPage = 1;
                result.PageSize = result.RowCount;
            }

            return result;
        }

        protected IQueryable<T> AddQueryProperties(IQueryable<T> query, params Expression<Func<T, object>>[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);

            return query;
        }

        public virtual async Task<PagedResult<T>> GetAllPagedAsync(CancellationToken cancellation, int page, int pageSize, params Expression<Func<T, object>>[] includeProperties) =>
            await PaginateQuery(
                AddQueryProperties(DbEntity, includeProperties),
                page, pageSize,
                cancellation
            );


        public virtual async Task<PagedResult<T>> GetAllPagedFilteredAsync(CancellationToken cancellation, int page, int pageSize, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties) =>
            await PaginateQuery(
                AddQueryProperties(DbEntity.Where(expr), includeProperties),
                page, pageSize,
                cancellation
            );

        public virtual async Task<IEnumerable<T>> QueryAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties) =>
            await AddQueryProperties(DbEntity.Where(expr), includeProperties)
                .ToListAsync(cancellation);

        public virtual async Task<IEnumerable<T>> QueryAsync(CancellationToken cancellation, bool tracking, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties) =>
            await AddQueryProperties(tracking ? DbEntity.Where(expr) : DbEntity.Where(expr).AsNoTracking(), includeProperties)
                .ToListAsync(cancellation);

        public virtual async Task<T> FirstOrDefaultAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties) =>
            await AddQueryProperties(DbEntity.Where(expr), includeProperties)
                .FirstOrDefaultAsync(cancellation);

        public virtual async Task<T> GetByKeyAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties) =>
            await AddQueryProperties(DbEntity.Where(expr), includeProperties)
                .FirstOrDefaultAsync(cancellation);

        public void Dispose()
        {
            DbConn.Dispose();
        }
    }
}
