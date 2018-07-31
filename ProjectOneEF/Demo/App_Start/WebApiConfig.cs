using Demo.Models;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using MongoDB.Driver;
using System.Linq;
using Microsoft.AspNet.OData.Builder;
using System.Web.Http;
using Microsoft.AspNet.OData.Batch;

namespace Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));

            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            config.EnsureInitialized();

        }
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "Demos";
            builder.ContainerName = "DefaultContainer";
            builder.EntitySet<Person>("People");
            builder.EntitySet<Trip>("Trips");
            builder.EntitySet<Models.MongoDB>("MongoDB");

            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
