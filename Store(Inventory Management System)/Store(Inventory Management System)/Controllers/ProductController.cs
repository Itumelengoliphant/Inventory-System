using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Store_Inventory_Management_System_;

namespace Store_Inventory_Management_System_.Controllers
{
    public class ProductController : Controller
    {
        private Forek_Institute_Inventory_DBEntities db = new Forek_Institute_Inventory_DBEntities();

        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.
                            Include(p => p.Category).
                            Include(p => p.Department).
                            Include(p => p.Supplier);
            return View(products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.DepId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.SuppId = new SelectList(db.Suppliers, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Quantity,Cost,DoesExpire,CatId,DepId,SuppId,Condition")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name", product.CatId);
            ViewBag.DepId = new SelectList(db.Departments, "Id", "Name", product.DepId);
            ViewBag.SuppId = new SelectList(db.Suppliers, "Id", "Name", product.SuppId);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name", product.CatId);
            ViewBag.DepId = new SelectList(db.Departments, "Id", "Name", product.DepId);
            ViewBag.SuppId = new SelectList(db.Suppliers, "Id", "Name", product.SuppId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Quantity,Cost,DoesExpire,CatId,DepId,SuppId,Condition")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name", product.CatId);
            ViewBag.DepId = new SelectList(db.Departments, "Id", "Name", product.DepId);
            ViewBag.SuppId = new SelectList(db.Suppliers, "Id", "Name", product.SuppId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetAllElectricalProducts()
        {
            var products = db.Products.
                            Include(p => p.Category).
                            Include(p => p.Department).
                            Include(p => p.Supplier);

            return View(products.Where(p => p.DepId == 1).ToList());
        }


        public JsonResult GetDataSearch(string SearchBy, string SearchValue)
        {
            List<Product> productList = new List<Product>();

            if (SearchBy == "ID")
            {
                try
                {
                    int Id = Convert.ToInt32(SearchValue);

                    productList = db.Products.Where(x => x.Id == Id || SearchValue == null).ToList();

                }
                catch (FormatException ex)
                {
                    throw ex;
                }
                return Json(productList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                productList = db.Products.Where(x => x.Name.Contains(SearchValue) || SearchValue == null).ToList();
                return Json(productList, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
