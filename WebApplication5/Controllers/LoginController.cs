using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Common;
namespace WebApplication5.Controllers
{
    public class LoginController : Controller
    {
        TravelDB db = new TravelDB();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authen(User user)
        {
            var password = Encrypt.MD5Hash(user.pwd);
            var check = db.Users.Where(s => s.numberphone== user.numberphone && s.pwd == password).FirstOrDefault();
            if (check == null)
            {
                return View("Index", user);
            }
            else
            {
                Session["Numberphone"] = user.numberphone;
                Session["IDUser"] = user.id;
                return RedirectToAction("Index","Home");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}