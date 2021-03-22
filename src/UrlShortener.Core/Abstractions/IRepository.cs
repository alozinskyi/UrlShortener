using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Core.Models;

namespace UrlShortener.Core.Abstractions
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Guid id);

        Task AddAsync(TEntity entity);

        Task UpdateAsync(Guid id, TEntity entity);

        Task<TEntity> SearchFirstAsync<TValue>(string field, TValue value);

        Task DeleteAsync(Guid id);

        Task<TEntity> IncreaseFieldValueAsync<TValue>(Guid id, string fieldName, TValue value);
    }
}
