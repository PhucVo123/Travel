using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        TravelDB _db = new TravelDB();
        // GET: Service
        public ActionResult Index(string meta)
        {
            var v = from t in _db.News
                    where t.meta == meta
                    select t;

            return View(v.FirstOrDefault());
        }

        public ActionResult DetailNews(long id)
        {
            var v = from t in _db.News
                    where t.id == id
                    orderby t.order ascending
                    select t;
            return View(v.FirstOrDefault());
        }

    }
}