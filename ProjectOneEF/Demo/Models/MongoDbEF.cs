using Blueshift.EntityFrameworkCore.MongoDB.Annotations;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemoII.Models
{
    [MongoDatabase("PIM_db")]
    public class PIM_dbContext : DbContext
    {
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
            var connectionString = "mongodb://localhost";
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