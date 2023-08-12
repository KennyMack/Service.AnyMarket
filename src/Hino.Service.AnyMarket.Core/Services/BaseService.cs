using Hino.Service.AnyMarket.Core.Interfaces;
using Hino.Service.AnyMarket.Entities;
using Hino.Service.AnyMarket.Logs;
using Hino.Service.AnyMarket.Utils.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hino.Service.AnyMarket.Core.Services
{
    public class BaseService<T> :
        IBaseWriterService<T>,
        IBaseReaderService<T>
        where T : BaseEntity
    {
        public List<string> Errors { get; set; }
        protected IBaseWriterRepository<T> DataRepositoryWriter { get; }
        protected IBaseReaderRepository<T> DataRepositoryReader { get; }

        public BaseService(
            IBaseWriterRepository<T> repoWriter,
            IBaseReaderRepository<T> repoReader)
        {
            DataRepositoryWriter = repoWriter;
            DataRepositoryReader = repoReader;
            Errors = new List<string>();
        }

        public BaseService(
           IBaseReaderRepository<T> repoReader)
        {
            DataRepositoryReader = repoReader;
            Errors = new List<string>();
        }

        public virtual T Add(T model)
        {
            try
            {
                DataRepositoryWriter.Add(model);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);
            }
            return model;
        }

        public virtual T Update(T model)
        {
            try
            {
                DataRepositoryWriter.Update(model);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);
            }
            return model;
        }

        public virtual async Task<T> RemoveByKeyAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr)
        {
            try
            {
                var model = await DataRepositoryWriter.RemoveByKeyAsync(cancellation, expr);
                return model;
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);
            }
            return default;
        }

        public virtual T Remove(T model)
        {
            try
            {
                DataRepositoryWriter.Remove(model);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);
            }
            return model;
        }

        public virtual async Task<T> FirstOrDefaultAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return await DataRepositoryReader.FirstOrDefaultAsync(cancellation, expr, includeProperties);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);

                return null;
            }
        }

        public virtual async Task<PagedResult<T>> GetAllPagedAsync(CancellationToken cancellation, int page, int pageSize, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return await DataRepositoryReader.GetAllPagedAsync(cancellation, page, pageSize, includeProperties);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);

                return null;
            }
        }

        public virtual async Task<PagedResult<T>> GetAllPagedFilteredAsync(CancellationToken cancellation, int page, int pageSize, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return await DataRepositoryReader.GetAllPagedFilteredAsync(cancellation, page, pageSize, expr, includeProperties);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);

                return null;
            }
        }

        public virtual async Task<T> GetByKeyAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return await DataRepositoryReader.GetByKeyAsync(cancellation, expr, includeProperties);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);

                return null;
            }
        }

        public virtual async Task<IEnumerable<T>> QueryAsync(CancellationToken cancellation, Expression<Func<T, bool>> expr, params Expression<Func<T, object>>[] includeProperties)
        {
            try
            {
                return await DataRepositoryReader.QueryAsync(cancellation, expr, includeProperties);
            }
            catch (Exception e)
            {
                Logger.LogError(e.Message, e);
                Errors.Add(e.Message);

                return null;
            }
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellation)
        {
            var countReturn = -1;
            try
            {
                countReturn = await DataRepositoryWriter.SaveChangesAsync(cancellation);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                Errors.Add(ex.Message);
                return countReturn;
            }
            return countReturn;
        }

        public void Dispose()
        {
            DataRepositoryReader?.Dispose();
            DataRepositoryWriter?.Dispose();
        }
    }
}
