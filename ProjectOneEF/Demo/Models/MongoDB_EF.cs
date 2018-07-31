using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blueshift.EntityFrameworkCore.MongoDB.Annotations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;


namespace Demo.Models
{
    public class MongoDB_EF
    {
    }

    [MongoDatabase("PIM_db")]
    public class PIM_dbContext : DbContext
    {
        public DbSet<MongoDbEF> Entry { get; set; }

        public PIM_dbContext()
            : this(new DbContextOptions<PIM_dbContext>())
        {
        }

        public PIM_dbContext(DbContextOptions<PIM_dbContext> PIM_dbContextOptions)
            : base(PIM_dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "mongodb://localhost:32768/";
            //optionsBuilder.UseMongoDb(connectionString);

            var mongoUrl = new MongoUrl(connectionString);
            //optionsBuilder.UseMongoDb(mongoUrl);

            MongoClientSettings settings = MongoClientSettings.FromUrl(mongoUrl);
            //settings.SslSettings = new SslSettings
            //{
            //    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
            //};
            //optionsBuilder.UseMongoDb(settings);

            MongoClient mongoClient = new MongoClient(settings);
            optionsBuilder.UseMongoDb(mongoClient);
        }
    }
    //[BsonIgnoreExtraElements]
    public class MongoDbEF
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
        public List<MongoDBsub> DisplayAttribute { get; set; }
        //public BsonDocument CatchAll { get; set; }
        //public BsonDocument ExtraElements { get; set; }
    }

    public class MongoDbEFsub
    {
        [BsonExtraElements]
        public Dictionary<string, Object> OtherDataSub { get; set; }
        public List<MongoDBsubsub> DisplayAttributeSub { get; set; }
    }

    public class MongoDbEFsubsub
    {
        [BsonExtraElements]
        public Dictionary<string, Object> OtherDataSub { get; set; }
    }
}