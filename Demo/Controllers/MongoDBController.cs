using Demo.DataSource;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.OData;
using System.Web.Script.Serialization;

namespace Demo.Controllers
{
    [EnableQuery]
    public class MongoDBController : ODataController
    {
        public async Task<IHttpActionResult> Get()
        {

            var client = new MongoClient("mongodb://localhost:32768/");

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

            var collection = database.GetCollection<BsonDocument>("same_structure");// AsQueryable<Zip>();

            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict }; // key part


            var filter = new BsonDocument();
            var allDocuments = new List<Models.MongoDB>();
            //var allDocuments = new List<String>();

            using (var cursor = await collection.Find(filter).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var document in cursor.Current)
                    {
                        var ser = BsonSerializer.Deserialize<Models.MongoDB>(document);
                        //var ser = BsonSerializer.Deserialize<Models.MongoDB>(document).ToJson(jsonWriterSettings);
                        //Models.MongoDB obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.MongoDB>(ser.ToJson(jsonWriterSettings));
                        allDocuments.Add(ser);

                        //System.Diagnostics.Debug.WriteLine(ser.ToJson(jsonWriterSettings));

                    }
                }
            }
            /*
            using (var cursor = collection.FindSync(new BsonDocument()))
            {
                foreach (var document in cursor.ToEnumerable())
                {
                    allDocuments.Add(BsonSerializer.Deserialize<Models.MongoDB>(document));

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
            */

            //System.Diagnostics.Debug.WriteLine(allDocuments);
            //System.Diagnostics.Debug.WriteLine(collection.FindSync(new BsonDocument()).ToList());




            //var documents = await collection.FindAsync(new BsonDocument());

            //System.Diagnostics.Debug.Write(documents.ToString());
            //System.Diagnostics.Debug.WriteLine(allDocuments.ToJson(jsonWriterSettings).AsQueryable());
            return Ok(allDocuments.AsQueryable());
        }
    }
}