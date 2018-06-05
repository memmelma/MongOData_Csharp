using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class sameStructure
    {
        //[Key]
        //public String _id { get; }
        //public String city { get; set; }
        //public IList<int> loc { get; set; }
        //[Key]
        //public int pop { get; set; }
        //public String state { get; set; }

        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String _id { get; set;  }
        [BsonRepresentation(BsonType.Int32)]
        public int? a { get; set; } = null;
        [BsonRepresentation(BsonType.Int32)]
        public int? b { get; set; } = null;
        [BsonRepresentation(BsonType.Int32)]
        public int? c { get; set; } = null;
    }
}