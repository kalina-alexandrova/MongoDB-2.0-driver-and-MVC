using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using Products.DataLayer;
using Products.Services;
using System.Configuration;


namespace DataLayer.Services
{
    public class ProductService : EntityService<Product> 
    {
        public async Task<IEnumerable<Product>> getAllProducts()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["dblocation"].ToString());
            var database = client.GetDatabase(ConfigurationManager.AppSettings["dbname"].ToString());
            var collection = database.GetCollection<Product>("product");

            var documents = collection.Find(_ => true).ToListAsync();//.ContinueWith(e=>e.Result.AsEnumerable());
            documents.Wait();
            var doc = documents.Result.AsEnumerable();
            IEnumerable<Product> result = (IEnumerable<Product>)doc;
            return result;
        }

        public override void Update(Product entity)
        {
            var filter = Builders<Product>.Filter.Eq(s => s.Id, entity.Id);
            var update = Builders<Product>.Update
                .Set(s => s.Title, entity.Title)
                .Set(s => s.Description, entity.Description)
                .Set(s => s.Price, entity.Price)
                .Set(s => s.IsActive, entity.IsActive)
                //.Set(s => s.ImageName, entity.ImageName)
                .Set(s => s.CategoryId, entity.CategoryId);
            var result = this.MongoConnectionHanler.MongoCollection.UpdateOneAsync(filter, update);
        }
    }
}