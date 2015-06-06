using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Products.DataLayer;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Configuration;



namespace DataLayer.Connection
{
    public class MongoConnectionHandler<T> where T : IMongoEntity
    {
        public IMongoCollection<T> MongoCollection { get; private set; }

        public MongoConnectionHandler()
        {
            var connectionString = ConfigurationManager.AppSettings["dblocation"].ToString();

            var mongoClient = new MongoClient(connectionString);

            var db = mongoClient.GetDatabase(ConfigurationManager.AppSettings["dbname"].ToString());
            MongoCollection = db.GetCollection<T>(typeof(T).Name.ToLower());
        }

    }
}