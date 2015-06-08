using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Products.DataLayer;
using DataLayer.Services;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.Web.Helpers;
using System.IO;

namespace Products.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Create()
        {
            var categoryService = new ProductCategoryService();
            var categoryDetails = categoryService.getAllCategories().Result;

            Product model = new Product();
            model.Categories = categoryDetails;
            //model.CategoryId = " ";
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpPostedFileBase file = Request.Files[0]; //Uploaded file
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;

                    var image = WebImage.GetImageFromRequest();

                    if (image != null)
                    {
                       if (image.Width > 500)
                       {
                           image.Resize(500, ((500 * image.Height) / image.Width));
                       }

                       image.Save(System.IO.Path.Combine(
                                  Server.MapPath("~/images"), image.FileName));

                    }

                    var productService = new ProductService();

                    product.ImageName = file.FileName;

                    productService.Create(product);
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(string name)
        {
            try
            {
                var productService = new ProductService();
                productService.Delete(name);

                return Json(new { status = true });
            }
            catch
            {
                return Json(new { status = false });
            }
        }

        public async Task<ActionResult> Edit(string name)
        {
            var productService = new ProductService();
            var product = await productService.GetByName(name);
            var categoryService = new ProductCategoryService();
            var categoryDetails = categoryService.getAllCategories().Result;
            product.Categories = categoryDetails;
            
            return View(product);
        
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var productService = new ProductService();
                    productService.Update(product);
                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Details(string name)
        {
            var productService = new ProductService();
            var product = await productService.GetByName(name);

            var categoryService = new ProductCategoryService();
            var categoryDetails = categoryService.getAllCategories().Result;
            product.Categories = categoryDetails;
            return View(product);
        }

        public ActionResult Index()
        {
            var productService = new ProductService();
            var productDetails = productService.getAllProducts().Result;

            return View(productDetails);
        }

        
        

    }
}
