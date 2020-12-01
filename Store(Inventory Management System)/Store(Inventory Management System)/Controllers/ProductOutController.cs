using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using Store_Inventory_Management_System_;
using Store_Inventory_Management_System_.Models;

namespace Store_Inventory_Management_System_.Controllers
{
    public class ProductOutController : Controller
    {
        private Forek_Institute_Inventory_DBEntities db = new Forek_Institute_Inventory_DBEntities();

        // GET: ProductOut
        public ActionResult Index()
        {
            var productOuts = db.ProductOuts.Include(p => p.Product).Include(p => p.User);
            return View(productOuts.ToList());
        }

        // GET: ProductOut/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOut productOut = db.ProductOuts.Find(id);
            if (productOut == null)
            {
                return HttpNotFound();
            }
            return View(productOut);
        }

        // GET: ProductOut/Create
        public ActionResult Create()
        {
            ViewBag.ProdId = new SelectList(db.Products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProdId,UserId,Quantity,Date,Reason")] ProductOut productOut)
        {
            if (ModelState.IsValid)
            {

                int totalProducts = db.Products.
                            Where(p => p.Id == productOut.ProdId).
                            Select(p => p.Quantity).
                            FirstOrDefault();

                int netQuantity = totalProducts - productOut.Quantity;

                if(productOut.Quantity > 0)
                {
                    if(productOut.Quantity <= totalProducts)
                    {
                        db.ProductOuts.Add(productOut);
                        db.SaveChanges();
                        UpdateProductQuantity(productOut.ProdId, netQuantity);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error: 'Quantity' cannot exceed available items!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Error: 'Quantity' cannot be 0 or less!");

                }
            }

            ViewBag.ProdId = new SelectList(db.Products, "Id", "Name", productOut.ProdId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productOut.UserId);
            return View(productOut);
        }

        // GET: ProductOut/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOut productOut = db.ProductOuts.Find(id);
            if (productOut == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdId = new SelectList(db.Products, "Id", "Name", productOut.ProdId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productOut.UserId);
            return View(productOut);
        }

        // POST: ProductOut/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProdId,UserId,Quantity,Date,Reason")] ProductOut productOut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProdId = new SelectList(db.Products, "Id", "Name", productOut.ProdId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", productOut.UserId);
            return View(productOut);
        }

        // GET: ProductOut/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOut productOut = db.ProductOuts.Find(id);
            if (productOut == null)
            {
                return HttpNotFound();
            }
            return View(productOut);
        }

        // POST: ProductOut/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductOut productOut = db.ProductOuts.Find(id);
            db.ProductOuts.Remove(productOut);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Reports(string ReportType)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Server.MapPath("~/Reports/Product.rdlc");

            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            reportDataSource.Value = db.Products.ToList();
            localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeTime;
            string encoding;
            string fileNameExtension;

            if (reportType == "Excel")
            {
                fileNameExtension = "xlsx";
            }
            else if (reportType == "Word")
            {
                fileNameExtension = "docx";
            }

            else if (reportType == "PDF")
            {
                fileNameExtension = "pdf";
            }
            else
            {
                fileNameExtension = "jpg";
            }



            string[] streams;
            Warning[] warnings;
            byte[] renderdByte;

            renderdByte = localReport.Render(reportType, "", out mimeTime, out encoding, out fileNameExtension, out streams, out warnings);


            Response.AddHeader("content-disposition", "attachment;fileName=ProductReport." + fileNameExtension);
            return File(renderdByte, fileNameExtension);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        private void UpdateProductQuantity(int id, int quantity)
        {
            Product product = new Product
            {
                Id = id,
                Quantity = quantity
            };

            db.Products.Attach(product);
            db.Entry(product).Property(x => x.Quantity).IsModified = true;
            db.SaveChanges();
        }
    }
}
