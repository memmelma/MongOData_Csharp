using Demo.DataSource;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.OData;

namespace Demo.Controllers
{   [EnableQuery]
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
}