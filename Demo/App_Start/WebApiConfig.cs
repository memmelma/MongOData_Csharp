using Demo.Models;
using Microsoft.OData.Edm;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace Demo
{
    public static class WebApiConfig
    {
        public static async void Register(HttpConfiguration config)
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
            builder.EntitySet<sameStructure>("sameStructures");
            //builder.EntitySet<sameStructure>("sameStructures").EntityType.HasKey(entity => entity.a);
            var edmModel = builder.GetEdmModel();
            return edmModel;
        }
    }
}
