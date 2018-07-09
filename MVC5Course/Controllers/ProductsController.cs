using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModel;
using Omu.ValueInjecter;
namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
      

        // GET: Products
        public ActionResult Index()
        {
            var data = _db.Product.OrderByDescending(p=>p.ProductId).Take(10).ToList();
            return View(data);
        }
        public ActionResult Index2()
        {
            var data = _db.Product
                .Where( p=>p.Active==true)
                .OrderByDescending(p => p.ProductId)
                .Take(10)
                .Select(p => new ProductViewModel()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock
                });
            return View(data);
        }
        public ActionResult AddnewProduct()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddnewProduct(ProductViewModel data)
        {
           
            if (!ModelState.IsValid)
            {
                return View();
            }

            var product = new Product()
            {
                ProductName = data.ProductName,
                Active = true,
                Price = data.Price,
                Stock = data.Stock
            };
            _db.Product.Add(product);
            _db.SaveChanges();
            
            return RedirectToAction("Index2");
        }
        public ActionResult EditProduct(int id)
        {
            var data = _db.Product.Find(id); // id 通常為主索引


            return View(data);
        }
        [HttpPost]
        public ActionResult EditProduct(Product data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            var one = _db.Product.Find(data.ProductId);
            one.InjectFrom(data);
            _db.SaveChanges();
            return RedirectToAction("Index2");
        }

        public ActionResult DeleteProduct(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = _db.Product.Find(id);
            

            return View(data);
        }
        [HttpPost]
        public ActionResult DeleteProduct(int id , string act)
        {
           
            if (act != "del")
            {
                return View();
            }
            var product = _db.Product.Find(id);
            if (product != null)
            {
                _db.Product.Remove(product);
                _db.SaveChanges();
             
            }

            return RedirectToAction("Index2");

        }
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Product.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _db.Product.Find(id);
            _db.Product.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
