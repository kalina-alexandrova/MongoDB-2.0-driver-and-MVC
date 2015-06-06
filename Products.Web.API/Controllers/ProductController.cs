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
    public class ProductController : ApiController
    {
        
        [HttpGet]
        [Route("api/products")]
        public HttpResponseMessage Products()
        {
            var service = new ProductService();
            var items = service.getAllProducts().Result;

            var totalRecords = items.Count();
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());

            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        #region Product Actions

        // GET 
        [HttpGet]
        [Route("api/products/productByName/{productName}")]
        public HttpResponseMessage ProductByName(string productName)
        {
            var service = new ProductService();
            var prod = service.GetByName(productName);
            return Request.CreateResponse(HttpStatusCode.OK, prod);
        }

        [HttpPut]
        [Route("api/products/updateproduct/{productName}")]
        public HttpResponseMessage UpdateCategory(string productName, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                var service = new ProductService();
                service.Update(product);
                return Request.CreateResponse<Product>(HttpStatusCode.Accepted, product);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, "Model state is not valid!");
        }

        [HttpPost]
        [Route("api/products/createproduct")]
        public HttpResponseMessage CreateCategory([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                var service = new ProductService();
                service.Create(product);
                var response = Request.CreateResponse<Product>(HttpStatusCode.Created, product);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Model state is not valid!");
        }

        #endregion Product Actions
    }
}