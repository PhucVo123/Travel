using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using WebApplication5.Common;
namespace WebApplication5.Controllers
{
    public class RegisterController : Controller
    {
        TravelDB _db = new TravelDB();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                var check = _db.Users.FirstOrDefault(s => s.numberphone == user.numberphone);
                var confirm = Encrypt.MD5Hash(user.confirm_pwd);
                var password = Encrypt.MD5Hash(user.pwd);
                if (check == null && password == confirm) 
                {
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    user.id = getMaxId();
                    user.pwd = password;
                    user.confirm_pwd = confirm;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index","Home"); 
                }
                else
                {
                    ViewBag.error = "Number Phone is existed";
                    return View();
                } 
            }
            return View();
        }
        public int getMaxId()
        {
            return _db.Users.Count();
        }
    }
}