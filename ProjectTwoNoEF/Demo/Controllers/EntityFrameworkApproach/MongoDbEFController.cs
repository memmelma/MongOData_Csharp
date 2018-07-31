﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.OData;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace Demo.Controllers
{
    [EnableQuery]
    public class MongoDbEFController : ODataController
    {
        public IMongoQueryable<Models.MongoDbModel> Get(ODataQueryOptions opts)
        {
            using (var db = new PIM_dbContext())
            {
                db.Add(new Models.MongoDbModel { _id = "id1234" });
                System.Diagnostics.Debug.WriteLine("got here!");
                db.SaveChanges();
            }

            var client = new MongoClient("mongodb://localhost:32768/");

            var database = client.GetDatabase("PIM_db");

            var collection = database.GetCollection<Models.MongoDbModel>("product_data");// AsQueryable<Zip>();

            //var x = opts.ApplyTo(collection.AsQueryable(), new ODataQuerySettings());
            //System.Diagnostics.Debug.WriteLine(x);

            //var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }; // key part


            //return Ok(collection.AsQueryable().Where(new BsonDocument()));

            //-->opts.ApplyTo(collection.AsQueryable());

            //validate queryopts -> validator!

            var x = collection.AsQueryable();


            return collection.AsQueryable();
            //return x;
        }
    }
}