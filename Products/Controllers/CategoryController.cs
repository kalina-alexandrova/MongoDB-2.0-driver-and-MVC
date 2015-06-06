using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Products.DataLayer;
using DataLayer.Services;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Products.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Create()
        {
            return View(new ProductCategory());
        }


        [HttpPost]
        public ActionResult Create(ProductCategory category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var categoryService = new ProductCategoryService();
                    categoryService.Create(category);
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
        public ActionResult Delete(ProductCategory category)
        {
            try
            {
                var categoryService = new ProductCategoryService();
                categoryService.Delete(category.Id.ToString());
                return View("Index",category);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Edit(string name)
        {
            var categoryService = new ProductCategoryService();
            var category = await categoryService.GetByName(name);
            categoryService.Update(category);

            return View(category);
        }
 
        //get all
        
        public ActionResult Index()
        {
            var categoryService = new ProductCategoryService();
            var categoryDetails = categoryService.getAllCategories().Result;

            //WebClient client = new WebClient();
            //var stream = client.DownloadString(new Uri("http://localhost:57383/api/categories"));
            //IEnumerable<ProductCategory> list = JsonConvert.DeserializeObject<IEnumerable<ProductCategory>>(stream);
            
            return View(categoryDetails);
        }


    }
}
