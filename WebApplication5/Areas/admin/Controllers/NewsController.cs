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
using WebApplication5.Models;

namespace WebApplication5.Areas.admin.Controllers
{
    public class NewsController : Controller
    {
        private TravelDB db = new TravelDB();

        // GET: admin/News
        public ActionResult Index()
        {
            return View(db.News.ToList());
        }

        // GET: admin/News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: admin/News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nameNews,content,nameOfAdmin,nameOfLink,linkNews,imgNews,dateRelease,meta,hide,order,datebegin")] News news, HttpPostedFileBase img)
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
                        news.imgNews = filename; //Lưu ý
                    }
                    else
                    {
                        news.imgNews = "logo.png";
                    }
                    news.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    news.meta = news.meta; //convert Tiếng Việt không dấu
                    news.id = getMaxId();
                    db.News.Add(news);
                    db.SaveChanges();
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

            return View(news);
        }

        // GET: admin/News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nameNews,content,nameOfAdmin,nameOfLink,linkNews,imgNews,dateRelease,meta,hide,order,datebegin")] News news, HttpPostedFileBase img)
        {
            try
            {
                var path = "";
                var filename = "";
                News temp = getById(news.id);
                if (ModelState.IsValid)
                {
                    if (img != null)
                    {
                        //filename = Guid.NewGuid().ToString() + img.FileName;
                        filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                        path = Path.Combine(Server.MapPath("~/Images/"), filename);
                        img.SaveAs(path);
                        temp.imgNews = filename; //Lưu ý
                    }
                    // temp.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());                   
                    temp.nameNews = news.nameNews;
                    temp.content = news.content;
                    temp.nameOfAdmin = news.nameOfAdmin;
                    temp.dateRelease = news.dateRelease;
                    temp.nameOfLink= news.nameOfLink;
                    temp.meta = news.meta; //convert Tiếng Việt không dấu
                    temp.hide = news.hide;
                    temp.order = news.order;
                    db.Entry(temp).State = EntityState.Modified;
                    db.SaveChanges();
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

            return View(news);
        }
        public News getById(long id)
        {
            return db.News.Where(x => x.id == id).FirstOrDefault();

        }
        // GET: admin/News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
        public int getMaxId()
        {
            return db.News.Count();
        }
    }
}
