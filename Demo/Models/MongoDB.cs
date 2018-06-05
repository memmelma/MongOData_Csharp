using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    //[BsonIgnoreExtraElements]
    public class MongoDB
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]       //both needed, when MongoDBs $oid (ObjectId) is used
        public String _id { get; set; }
        
        [BsonExtraElements]
        public Dictionary<string, Object> OtherData { get; set; }   //structures with more than 1 layer currently don't work
        //public BsonDocument CatchAll { get; set; }
    }
}