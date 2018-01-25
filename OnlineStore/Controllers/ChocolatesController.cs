using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class ChocolatesController : Controller
    {
        private OnlineStoreContext db = new OnlineStoreContext();

        // GET: Chocolates
        public ActionResult Index()
        {
            return View(db.Chocolates.ToList());
        }

        // GET: Chocolates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chocolate chocolate = db.Chocolates.Find(id);
            if (chocolate == null)
            {
                return HttpNotFound();
            }
            return View(chocolate);
        }

        // GET: Chocolates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Chocolates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChocolateId,CocoaContent,Name,Image,Price")] Chocolate chocolate)
        {
            if (ModelState.IsValid)
            {
                db.Chocolates.Add(chocolate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chocolate);
        }

        // GET: Chocolates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chocolate chocolate = db.Chocolates.Find(id);
            if (chocolate == null)
            {
                return HttpNotFound();
            }
            return View(chocolate);
        }

        // POST: Chocolates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChocolateId,CocoaContent,Name,Image,Price")] Chocolate chocolate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chocolate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chocolate);
        }

        // GET: Chocolates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chocolate chocolate = db.Chocolates.Find(id);
            if (chocolate == null)
            {
                return HttpNotFound();
            }
            return View(chocolate);
        }

        // POST: Chocolates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Chocolate chocolate = db.Chocolates.Find(id);
            db.Chocolates.Remove(chocolate);
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
