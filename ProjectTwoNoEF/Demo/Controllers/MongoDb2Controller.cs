using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.OData.Query;
using Demo.Models;
using Microsoft.AspNet.OData;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Demo.Controllers
{
    [EnableQuery]
    public class MongoDb2Controller : ODataController
    {
        //setup client, database connection, collection
        private static MongoClient client = new MongoClient("mongodb://localhost:32768/");
        private static IMongoDatabase db = client.GetDatabase("PIM_db");
        private static IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("product_data");

        public async Task<List<MongoDbModel>> Get(ODataQueryOptions opts)
        {
            //build a MongoDB filter/query, in this case empty query {}
            BsonDocument filter = new BsonDocument();
            List<MongoDbModel> allDocuments = new List<MongoDbModel>();

            //iterate over cursor array (collection) and deserialize the BSON document to the MongoDbModel
            using (var cursor = await collection.Find(filter).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var document in cursor.Current)
                    {
                        MongoDbModel ser = BsonSerializer.Deserialize<Models.MongoDbModel>(document);
                        allDocuments.Add(ser);

                    }
                }
            }

            //return all read documents
            return allDocuments;
        }
    }
}