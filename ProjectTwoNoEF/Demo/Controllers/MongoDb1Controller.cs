using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.OData;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Demo.Models;

namespace Demo.Controllers
{
    [EnableQuery]
    public class MongoDb1Controller : ODataController
    {
        //setup client, database connection, collection
        private static MongoClient client = new MongoClient("mongodb://localhost:32768/");
        private static IMongoDatabase db = client.GetDatabase("PIM_db");
        private static IMongoCollection<MongoDbModel> collection = db.GetCollection<Models.MongoDbModel>("product_data");

        public IMongoQueryable<Models.MongoDbModel> Get(/*ODataQueryOptions opts*/)
        {
            System.Diagnostics.Debug.WriteLine("get");
            //return collection as a queryable data structure
            return collection.AsQueryable();
        }

        [HttpPost]
        public async Task<IHttpActionResult> MongoDbAction([FromODataUri] int key, ODataActionParameters parameters)
        {
            System.Diagnostics.Debug.WriteLine("action");
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return new OkResult(new HttpRequestMessage());
        }
    }
}
