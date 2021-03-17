using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Core.Abstractions;
using UrlShortener.Core.Models;
using UrlShortener.DataAccess.MongoDb;

namespace UrlShortener.DataAccess.Repositories
{
    public class MongoDbRepository
    {
        public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
        {
            private readonly IMongoCollection<TEntity> _entities;

            public MongoRepository(IOptions<MongoDbSettings> settings)
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                var database = client.GetDatabase(settings.Value.DatabaseName);
                _entities = database.GetCollection<TEntity>($"{typeof(TEntity).Name}s");
            }

            public async Task AddAsync(TEntity entity)
            {
                await _entities.InsertOneAsync(entity);
            }

            public async Task DeleteAsync(Guid id)
            {
                await _entities.DeleteOneAsync(e => e.Id == id);
            }

            public async Task<IEnumerable<TEntity>> GetAllAsync()
            {
                return await _entities.Find(e => true).ToListAsync();
            }

            public async Task<TEntity> GetByIdAsync(Guid id)
            {
                return await _entities.Find(e => e.Id == id).FirstOrDefaultAsync();
            }

            public async Task UpdateAsync(Guid id, TEntity entity)
            {
                await _entities.ReplaceOneAsync(e => e.Id == entity.Id, entity);
            }

            public async Task<TEntity> SearchFirstAsync<TValue>(string fieldName, TValue value)
            {
                var filter = Builders<TEntity>.Filter.Eq(fieldName, value);
                return await _entities.Find(filter).FirstOrDefaultAsync();
            }
        }
    }
}
