using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web;
using System.Security.Claims;
using System.IO;
using System.Net.Http.Headers;
using Products.DataLayer;
using Products.Services;
using DataLayer.Services;

namespace Products.Web.API.Controllers
{
    //[RoutePrefix("api/categories")]
    public class ProductCategoryController : ApiController
    {
        
        [HttpGet]
        [Route("api/categories")]
        public HttpResponseMessage ProductCategories()
        {
            var service = new ProductCategoryService();
            var items = service.getAllCategories().Result;

            var totalRecords = items.Count();
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        #region Product Categories Actions

        // GET 
        [HttpGet]
        [Route("api/categories/productCategoryByName/{categoryName}")]
        public HttpResponseMessage CategoryByName(string categoryName)
        {
            var service = new ProductCategoryService();
            var cat = service.GetByName(categoryName);
            return Request.CreateResponse(HttpStatusCode.OK, cat);
        }

        [HttpPut]
        [Route("api/categories/updatecategory/{categoryName}")]
        public HttpResponseMessage UpdateCategory(string categoryName, [FromBody]ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                var service = new ProductCategoryService();
                service.Update(category);
                return Request.CreateResponse<ProductCategory>(HttpStatusCode.Accepted, category);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Model state is not valid!");
        }

        [HttpPost]
        [Route("api/categories/createcategory")]
        public HttpResponseMessage CreateCategory([FromBody]ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                var service = new ProductCategoryService();
                service.Create(category);
                var response = Request.CreateResponse<ProductCategory>(HttpStatusCode.Created, category);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Model state is not valid!");
        }

        #endregion Product Category Actions
    }
}