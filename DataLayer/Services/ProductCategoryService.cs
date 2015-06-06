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
using MongoDB.Bson.Serialization;
using System.Configuration;

namespace DataLayer.Services
{
    public class ProductCategoryService : EntityService<ProductCategory>
    {
        public async Task<IEnumerable<ProductCategory>> getAllCategories()
        {
            var client = new MongoClient(ConfigurationManager.AppSettings["dblocation"].ToString());
            var database = client.GetDatabase(ConfigurationManager.AppSettings["dbname"].ToString());
            var collection = database.GetCollection<ProductCategory>("productcategory");

            var documents = collection.Find(_ => true).ToListAsync();//.ContinueWith(e=>e.Result.AsEnumerable());
            documents.Wait();
            var doc = documents.Result.AsEnumerable();
            IEnumerable<ProductCategory> result = (IEnumerable<ProductCategory>)doc;
            return result;
        }

        
        public override void Update(ProductCategory entity)
        {
           
            var filter = Builders<ProductCategory>.Filter.Eq(s => s.Id, entity.Id);
            var update = Builders<ProductCategory>.Update
                .Set(s => s.Title, entity.Title)
                .Set(s => s.IsActive, entity.IsActive);
            var result = this.MongoConnectionHanler.MongoCollection.UpdateOneAsync(filter, update);         

        }
    }
}