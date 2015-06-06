using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using Products.DataLayer;
using System.Web.Mvc;

namespace Products.CustomModelBinders
{
    public class BsonObjectIdBinder : IModelBinder
    {
        public object BindModel(
            ControllerContext controllerContext,
            ModelBindingContext modelBindingContext)
        {
            var valueProviderResult = modelBindingContext.ValueProvider.GetValue(modelBindingContext.ModelName);
            return new ObjectId(valueProviderResult.AttemptedValue);
        }
    }

}