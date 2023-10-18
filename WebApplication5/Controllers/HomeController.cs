using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        TravelDB _db = new TravelDB();
      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
  
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Services()
        {
            return View();
        }
        public ActionResult getMenu()
        {
            var v = from t in _db.Menus
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());

        }
        public ActionResult getFooter()
        {
            var v = from t in _db.Footers
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());

        }
        //public ActionResult reg()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult SendOTP()
        //{
        //    int otpValue = new Random().Next(100000, 999999);
        //    var status = "";
        //    try
        //    {
        //        string recipient = ConfigurationManager.AppSettings["RecipientNumber"].ToString();
        //        string APIKey = ConfigurationManager.AppSettings["APIKey"].ToString();

        //        string message = "Your OTP is "+otpValue+" (Sent By: PhucVo)";
        //        String endcodeMessage = HttpUtility.UrlEncode(message) ;

        //        using(var webClient = new WebClient())
        //        {
        //            byte[] response = webClient.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
        //            {
        //                {"apikey", APIKey },
        //                {"numbers", recipient },
        //                {"message", endcodeMessage },
        //                {"sender", "TXTCLC" }
        //            });
        //            string result = System.Text.Encoding.UTF8.GetString(response) ;
        //            var jsonObject = JObject.Parse(result) ;

        //            status = jsonObject["status"].ToString() ;
        //            Session["CurrentOTP"] = otpValue;
        //        }
        //        return Json(status, JsonRequestBehavior.AllowGet) ;

        //    }
        //    catch(Exception e)
        //    {
        //        throw(e);
        //    }
        //}
        //public ActionResult EnterOTP()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public JsonResult VerifyOTP(string otp)
        //{
        //    bool result = false ;
        //    string sessionOTP = Session["CurrentOTP"].ToString() ;
        //    if(otp == sessionOTP)
        //    {
        //        result = true ;
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet) ;
        //}
        public ActionResult getServices()
        {
            var v = from t in _db.Services
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());

        }
        public ActionResult getPackages()
        {
            var v = from t in _db.Products
                    where t.hide == true && t.meta == "tour-du-lich"
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());

        }

        public ActionResult getNews()
        {
            var v = from t in _db.News
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());

        }
        public ActionResult getBlog()
        {
            
            var v = from t in _db.News
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());

        }

        public ActionResult getCategory()
        {
            ViewBag.meta = "dich-vu";
            var v = from t in _db.Categories
                    where t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());
            
        }
        public ActionResult getProduct(string meta)
        {
            ViewBag.meta = "dich-vu";
            var v = from t in _db.Products
                    where t.Category.meta == meta && t.hide == true
                    orderby t.order ascending
                    select t;
            return PartialView(v.ToList());

        }
       
    }
}