using Demo.DataSource;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;

namespace Demo.Controllers
{
    public class Controllers
    {
        [EnableQuery]
        public class PeopleController : ODataController
        {
            public IHttpActionResult Get()
            {
                return Ok(DemoDataSources.Instance.People.AsQueryable());
            }
        }

        [EnableQuery]
        public class TripsController : ODataController
        {
            public IHttpActionResult Get()
            {
                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }; // key part

                System.Diagnostics.Debug.WriteLine(DemoDataSources.Instance.Trips.ToJson(jsonWriterSettings));
                System.Diagnostics.Debug.WriteLine(DemoDataSources.Instance.Trips);

                return Ok(DemoDataSources.Instance.Trips.AsQueryable());
            }
        }

        [EnableQuery]
        public class sameStructuresController : ODataController
        {
            public IHttpActionResult Get()
            {

                var client = new MongoClient("mongodb://localhost:32768/");
                var allDocuments = new List<Models.sameStructure>();

                //using (var cursor = client.ListDatabases())
                //{
                //    foreach (var document in cursor.ToEnumerable())
                //    {
                //        System.Diagnostics.Debug.WriteLine(document.ToString());
                //    }
                //}

                var database = client.GetDatabase("test");

                //using (var cursor = database.ListCollections())
                //{
                //    foreach (var document in cursor.ToEnumerable())
                //    {
                //        System.Diagnostics.Debug.WriteLine(document.ToString());
                //    }
                //}

                var collection = database.GetCollection<BsonDocument>("inserts");// AsQueryable<Zip>();

                var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }; // key part

                using (var cursor = collection.FindSync(new BsonDocument()))
                {
                    foreach (var document in cursor.ToEnumerable())
                    {
                        allDocuments.Add(BsonSerializer.Deserialize<Models.sameStructure>(document));

                        System.Diagnostics.Debug.WriteLine(document);


                        //var myClass = new Models.sameStructure();
                        //try
                        //{
                        //    myClass.a = document["a"].AsInt32;
                        //}
                        //catch (Exception e) { continue; }
                        //try
                        //{
                        //    myClass.a = document["b"].AsInt32;
                        //}
                        //catch (Exception e) { continue; }
                        //try
                        //{
                        //    myClass.a = document["c"].AsInt32;
                        //}
                        //catch (Exception e){ continue; }
                        //myClass._id = (BsonId)document["_id"];

                        //document.ToJson(jsonWriterSettings);

                    }
                }
                

                System.Diagnostics.Debug.WriteLine(allDocuments);
                System.Diagnostics.Debug.WriteLine(collection.FindSync(new BsonDocument()).ToList());

                


                //var documents = await collection.FindAsync(new BsonDocument());

                //System.Diagnostics.Debug.Write(documents.ToString());
                
                return Ok(allDocuments.AsQueryable());
            }
        }
    }
}