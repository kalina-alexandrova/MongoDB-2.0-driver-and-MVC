using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;


namespace Products.DataLayer
{
    public class ProductCategory : MongoEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }
}