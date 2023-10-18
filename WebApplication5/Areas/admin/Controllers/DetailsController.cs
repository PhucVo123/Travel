using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Areas.admin.Controllers
{
    public class DetailsController : Controller
    {
        private TravelDB db = new TravelDB();

        // GET: admin/Details
        public ActionResult Index()
        {
            var details = db.Details.Include(d => d.Product);
            return View(details.ToList());
        }

        // GET: admin/Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detail detail = db.Details.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        // GET: admin/Details/Create
        public ActionResult Create()
        {
            ViewBag.ProductID = new SelectList(db.Products, "id", "namePackage");
            return View();
        }

        // POST: admin/Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,position_detail,facilities,depart,transit,starting,descDetail,scheduleDetail,ProductID,meta,hide,order,datebegin")] Detail detail)
        {
            if (ModelState.IsValid)
            {
                db.Details.Add(detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "id", "namePackage", detail.ProductID);
            return View(detail);
        }

        // GET: admin/Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Detail detail = db.Details.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "id", "namePackage", detail.ProductID);
            return View(detail);
        }

        // POST: admin/Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,position_detail,facilities,depart,transit,starting,descDetail,scheduleDetail,ProductID,meta,hide,order,datebegin")] Detail detail)
        { 
            try
            {

                var temp = db.Details.Find(detail.id);
                if (ModelState.IsValid)
                {
                    temp.position_detail = detail.position_detail;
                    temp.facilities = detail.facilities;
                    temp.transit =  detail.transit;
                    temp.starting = detail.starting;
                    temp.depart =   detail.depart;
                    temp.descDetail = detail.descDetail;
                    temp.scheduleDetail =   detail.scheduleDetail;
                    temp.hide = detail.hide;

                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index","Products");
                }
            }
            catch (DbEntityValidationException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ViewBag.ProductID = new SelectList(db.Products, "id", "namePackage", detail.ProductID);
            return View(detail);
        }

        // GET: admin/Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Detail detail = db.Details.Find(id);
            if (detail == null)
            {
                return HttpNotFound();
            }
            return View(detail);
        }

        // POST: admin/Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Detail detail = db.Details.Find(id);
            db.Details.Remove(detail);
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
