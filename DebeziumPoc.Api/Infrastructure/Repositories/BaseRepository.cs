using DebeziumPoc.Api.Domain.Repositories;
using DebeziumPoc.Api.Infrastructure.Context;
using MongoDB.Driver;
using MongoDBExample.Entities.BusinessEntities.Base;

namespace DebeziumPoc.Api.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly IMongoDBContext _mongoContext;
        protected IMongoCollection<T> _dbCollection;

        protected BaseRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<T>(typeof(T).Name);
        }

        public virtual T Add(T entity)
        {
            _dbCollection.InsertOne(entity);
            return entity;
        }

        public virtual T GetById(Guid id)
        {
            return _dbCollection.Find<T>(m => m.Id == id).FirstOrDefault();
        }

        public virtual void Remove(Guid id)
        {
            _dbCollection.DeleteOne(m => m.Id == id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbCollection.Find<T>(T => true).ToList();
        }

        public void Update(T entity)
        {
            _dbCollection.ReplaceOne(Builders<T>.Filter.Eq("_id", entity.Id), entity);
        }
    }
}
