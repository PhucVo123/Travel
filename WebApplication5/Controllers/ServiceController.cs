using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Dao;
using Newtonsoft.Json;
using System.Data.Entity.SqlServer;
//52000854 - Võ Huy Phúc

namespace WebApplication5.Controllers
{
    public class ServiceController : Controller
    {
        TravelDB db = new TravelDB();
        // GET: Service
        public ActionResult Index(string meta)
        {
            var v = from t in db.Categories
                    where t.meta == meta
                    select t;

            return View(v.FirstOrDefault());
        }
        public ActionResult Detail(long id)
        {
            var v = from t in db.Details
                    where t.ProductID == id && t.hide == true
                    orderby t.order_detail ascending
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult Search(string keyword, string date_checkin, string date_checkout, string numofpeople)
        {
            ViewBag.meta = "dich-vu";
            var result = from s in db.Products
                         where s.position == keyword || s.numberOfCustomer == (numofpeople + " người")
                         || s.dateStay == (SqlFunctions.DateDiff("day", date_checkin, date_checkout) + " ngày")
                         select s;
            return View(result.ToList());
        }
  
        //public ActionResult getDetailService(long id)
        //{
        //    var v = from t in _db.Details
        //            where  t.hide == true && t.ProductID == id
        //            orderby t.order_detail ascending
        //            select t;
        //    return PartialView(v.FirstOrDefault());
        //}
       
    }
}