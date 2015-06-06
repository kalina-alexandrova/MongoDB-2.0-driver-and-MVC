using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products.DataLayer
{
    [BsonIgnoreExtraElements]
    public class Product: MongoEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageName { get; set; }

        public ObjectId CategoryId { get; set; }

        public IEnumerable<ProductCategory> Categories { get; set; }

        public string State { get; set; }
    }
}