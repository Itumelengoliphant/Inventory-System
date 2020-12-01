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
    public class ItemReturnController : Controller
    {
        private Forek_Institute_Inventory_DBEntities db = new Forek_Institute_Inventory_DBEntities();

        // GET: ItemReturn
        public ActionResult Index()
        {
            var itemReturns = db.ItemReturns.Include(i => i.Department).Include(i => i.Department1).Include(i => i.Product).Include(i => i.User);
            return View(itemReturns.ToList());
        }

        // GET: ItemReturn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemReturn itemReturn = db.ItemReturns.Find(id);
            if (itemReturn == null)
            {
                return HttpNotFound();
            }
            return View(itemReturn);
        }

        // GET: ItemReturn/Create
        public ActionResult Create()
        {
            ViewBag.DeptRetId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.DeptTakenId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.ProdId = new SelectList(db.Products, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: ItemReturn/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ProdId,DeptTakenId,DeptRetId,Quantity,Date,Condition")] ItemReturn itemReturn)
        {
            if (ModelState.IsValid)
            {
                db.ItemReturns.Add(itemReturn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeptRetId = new SelectList(db.Departments, "Id", "Name", itemReturn.DeptRetId);
            ViewBag.DeptTakenId = new SelectList(db.Departments, "Id", "Name", itemReturn.DeptTakenId);
            ViewBag.ProdId = new SelectList(db.Products, "Id", "Name", itemReturn.ProdId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", itemReturn.UserId);
            return View(itemReturn);
        }

        // GET: ItemReturn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemReturn itemReturn = db.ItemReturns.Find(id);
            if (itemReturn == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptRetId = new SelectList(db.Departments, "Id", "Name", itemReturn.DeptRetId);
            ViewBag.DeptTakenId = new SelectList(db.Departments, "Id", "Name", itemReturn.DeptTakenId);
            ViewBag.ProdId = new SelectList(db.Products, "Id", "Name", itemReturn.ProdId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", itemReturn.UserId);
            return View(itemReturn);
        }

        // POST: ItemReturn/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ProdId,DeptTakenId,DeptRetId,Quantity,Date,Condition")] ItemReturn itemReturn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemReturn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptRetId = new SelectList(db.Departments, "Id", "Name", itemReturn.DeptRetId);
            ViewBag.DeptTakenId = new SelectList(db.Departments, "Id", "Name", itemReturn.DeptTakenId);
            ViewBag.ProdId = new SelectList(db.Products, "Id", "Name", itemReturn.ProdId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", itemReturn.UserId);
            return View(itemReturn);
        }

        // GET: ItemReturn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemReturn itemReturn = db.ItemReturns.Find(id);
            if (itemReturn == null)
            {
                return HttpNotFound();
            }
            return View(itemReturn);
        }

        // POST: ItemReturn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemReturn itemReturn = db.ItemReturns.Find(id);
            db.ItemReturns.Remove(itemReturn);
            db.SaveChanges();
            return RedirectToAction("Index");
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
