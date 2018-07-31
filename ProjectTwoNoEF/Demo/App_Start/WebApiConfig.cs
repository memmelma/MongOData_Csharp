using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Batch;
using Microsoft.AspNet.OData.Extensions;
using System.Web.Http;
using Demo.Models;


namespace Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapODataServiceRoute("odata", null, GetEdmModel(), new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));

            //configure allowed OData operators
            config.Count().Filter().OrderBy().Expand().Select().MaxTop(null);

            config.EnsureInitialized();

        }
        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.Namespace = "Demo";
            builder.ContainerName = "DefaultContainer";

            //register Route:/MongoDb1
            builder.EntitySet<Models.MongoDbModel>("MongoDb1");
            //register Route:/MongoDb2
            builder.EntitySet<Models.MongoDbModel>("MongoDb2");

            ActionConfiguration action = builder.EntityType<MongoDbModel>().Action("MongoDbAction");
            action.Parameter<string>("filter");
            action.Returns<string>();

            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
