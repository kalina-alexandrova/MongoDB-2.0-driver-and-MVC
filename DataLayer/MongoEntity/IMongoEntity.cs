using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.DataLayer
{
    public interface IMongoEntity
    {
        ObjectId Id { get; set; }
    }
}