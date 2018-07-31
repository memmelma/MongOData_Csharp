using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demo.Models
{
    //[BsonIgnoreExtraElements]
    public class MongoDbModel
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]       //both needed, when MongoDBs $oid (ObjectId) is used
        public String _id { get; set; }

        //[BsonElement("DisplayAttribute")]
        //public List<Dictionary<string, Object>> DisplayAttribute { get ; set; }
        //public String DisplayAttribute { get { return this.ToJson() }; set { }; }

        //[BsonRepresentation(BsonType.Array)]
        //public List<Dictionary<string, Object>> DisplayAttribute { get; set; }

        [BsonExtraElements]
        public Dictionary<string, Object> OtherData { get; set; }   //structures with more than 1 layer currently don't work
        public List<MongoDbLayerOne> DisplayAttribute { get; set; }
        //public BsonDocument CatchAll { get; set; }
        //public BsonDocument ExtraElements { get; set; }
    }

    public class MongoDbLayerOne
    {
        [Key]
        [BsonExtraElements]
        public Dictionary<string, Object> OtherDataSub { get; set; }
        public List<MongoDbLayerTwo> DisplayAttributeSub { get; set; }
    }

    public class MongoDbLayerTwo
    {
        [Key]
        [BsonExtraElements]
        public Dictionary<string, Object> OtherDataSub { get; set; }
    }

}