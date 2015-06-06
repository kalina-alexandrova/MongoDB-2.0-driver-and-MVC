using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Products.DataLayer;
using Products.Services;
using DataLayer.Connection;
using System.Threading.Tasks;

namespace Products.Services
{
    public abstract class EntityService<T>: IEntityService<T> where T: IMongoEntity
    {
        protected readonly MongoConnectionHandler<T> MongoConnectionHanler;

        public virtual void Create(T entity) {
            var result = this.MongoConnectionHanler.MongoCollection.InsertOneAsync(entity);

        }
        public virtual void Delete(string name) {
            var filter = Builders<T>.Filter.Eq("Title", name);
            var result = this.MongoConnectionHanler.MongoCollection.DeleteOneAsync(filter);
        }
        public async virtual Task<T> GetByName(string Name)
        {
            var filter = Builders<T>.Filter.Eq("Title", Name);
            return await this.MongoConnectionHanler.MongoCollection.Find(filter).FirstOrDefaultAsync();

        }

        public virtual void Update(T entity) {}
       

        protected EntityService()
        {
            MongoConnectionHanler = new MongoConnectionHandler<T>();
        }
    }
}