using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace WorkDayLog.Core
{
    public class RepositoryBase<T> 
        where T : EntityBase
    {
        private readonly IMongoDatabase _db;
        private readonly string _collectionName;

        protected RepositoryBase(IConfiguration config, string collectionName)
        {
            var connectionString = config.GetConnectionString("MongoDBConnection");

            var mongoDBClient = new MongoClient(connectionString);
            _db = mongoDBClient.GetDatabase("workdaylogdb");

            _collectionName = collectionName;
        }

        public virtual void Add(T entity)
            =>  _db.GetCollection<T>(_collectionName).InsertOne(entity);   

        public virtual void Update(T entity)
            =>  _db.GetCollection<T>(_collectionName).FindOneAndReplace(x => x.Id == entity.Id, entity);   

        public virtual IEnumerable<T> GetBy(Expression<Func<T, bool>> search)
            => _db.GetCollection<T>(_collectionName).Find(search, new FindOptions()).ToList();

        public virtual T GetFirstBy(Expression<Func<T, bool>> search)
            => _db.GetCollection<T>(_collectionName).Find(search, new FindOptions()).FirstOrDefault();

        public virtual T GetById(Guid id)
            => _db.GetCollection<T>(_collectionName).Find(x => x.Id == id).FirstOrDefault();
    }
}