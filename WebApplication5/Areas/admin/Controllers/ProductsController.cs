using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Help;
using WebApplication5.Models;

namespace WebApplication5.Areas.admin.Controllers
{
    public class ProductsController : Controller
    {
        private TravelDB db = new TravelDB();

        // GET: admin/Categories
        public ActionResult Index(long? id = null)
        {
            getCategory(id);
            return View();
        }
        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.Categories.Where(x => x.hide == true).OrderBy(x => x.order), "id", "nameCategory", selectedId);
        }
        public ActionResult getProduct(long? id)
        {
            if (id == null)
            {
                var v = db.Products.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.Products.Where(x => x.CategoryID == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }
        // GET: admin/Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            //var list = db.Products.Include("Categories").ToList();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: admin/Categories/Create
        public ActionResult Create()
        {
            getCategory();
            return View();
        }

        // POST: admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(ServiceModel serviceModel, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Images"), filename);
                        img.SaveAs(path);
                        serviceModel.img = filename; //Lưu ý
                    }
                    else
                    {
                        serviceModel.img = "logo.png";
                    }
                    Product product = new Product();
                    product.id = getMaxIdDetail() + 1;
                    product.namePackage = serviceModel.namePackage;
                    product.price = serviceModel.price;
                    product.star = serviceModel.star;
                    product.position = serviceModel.position;
                    product.dateStay = serviceModel.dateStay;
                    product.numberOfCustomer = serviceModel.numberOfCustomer;
                    product.area = serviceModel.area;
                    product.hide = serviceModel.hide;
                    product.img = serviceModel.img;
                    product.order = getMaxOrder(product.CategoryID);
                    product.meta = serviceModel.meta;
                    product.CategoryID = serviceModel.CategoryID;
                    db.Products.Add(product);
                    db.SaveChanges();

                    int lastestid = product.id;
                    //product.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    Detail detail = new Detail();
                    detail.descDetail = serviceModel.descDetail;
                    detail.scheduleDetail = serviceModel.scheduleDetail;
                    detail.hide = serviceModel.hide;
                    detail.meta_detail = serviceModel.meta_detail;
                    detail.id = getMaxIdDetail() + 1;
                    detail.ProductID = lastestid;
                    db.Details.Add(detail);
                    db.SaveChanges();



                    //return RedirectToAction("Index");
                    return RedirectToAction("Index");
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
            return View(serviceModel);
        }

        // GET: admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //db.Details.Where(s => s.ProductID == id).FirstOrDefault();
            Product product = db.Products.Find(id);
            if (product== null)
            {
                return HttpNotFound();
            }
            getCategory(product.CategoryID);
            return View(product);
        }

        // POST: admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,namePackage,position,dateStay,numberOfCustomer,star,price,CategoryID,area,img,meta,hide,order,datebegin")] Product product, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                var temp = db.Products.Find(product.id);


                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Images"), filename);
                        img.SaveAs(path);
                        temp.img = filename; //Lưu ý
                    }

                    //temp.Product.namePackage = detail.Product.namePackage;

                    temp.namePackage = product.namePackage;
                    temp.price = product.price;
                    temp.position = product.position;
                    temp.area = product.area;
                    temp.dateStay = product.dateStay;
                    temp.star = product.star;
                    temp.hide = product.hide;
                    temp.meta = Functions.ConvertToUnSign(product.meta); //convert Tiếng Việt không dấu
                    temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Index", "Products", new {id = product.CategoryID});
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

            return View(product);
        }


        // GET: admin/Categories/Delete/5
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

        // POST: admin/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            Detail detail = db.Details.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
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
        public int getMaxOrder(long? CategoryId)
        {
            if (CategoryId == null)
                return 1;
            return db.Products.Where(x => x.CategoryID == CategoryId).Count();
        }
        public int getMaxId()
        {
            int id = 20;
            return id;
        }
        public int getMaxIdDetail()
        {
            return db.Details.Count();
        }
    }
}

