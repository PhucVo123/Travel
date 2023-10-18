using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private const string CartSession = "CartSession";
        TravelDB db = new TravelDB();
       
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Delete(int id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.product.id == id);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.product.id == item.product.id);
                if (jsonItem != null)
                {
                    item.quantity = jsonItem.quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(int proID, int quantity)
        {
            var cart = Session[CartSession];
            var product_detail = db.Products.Find(proID);
            if (cart != null)
            {
                var list = (List<CartItem>)cart;

                if (list.Exists(x => x.product.id == proID))
                {
                    foreach (var item in list)
                    {
                        if (item.product.id == proID)
                        {
                            item.quantity += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.product = product_detail;
                    item.quantity = quantity;
                    list.Add(item);

                }
                Session[CartSession] = list;
            }
            else
            {
                var item = new CartItem();
                item.product = product_detail;
                item.quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }

            return RedirectToAction("Index");
        }
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string shipPhone, string shipAddress, string shipEmail)
        {
            Order order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipMobile = shipPhone;
            order.ShipAddress = shipAddress;
            order.ShipEmail = shipEmail;
            db.Orders.Add(order);
            db.SaveChanges();

            var id = order.id;
            try
            {
                var cart = (List<CartItem>)Session[CartSession];
                foreach (var item in cart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.id_pro = item.product.id;
                    orderDetail.id_order = id;
                    orderDetail.quanity = item.quantity;
                    orderDetail.price = 0;
                    db.OrderDetails.Add(orderDetail);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
                
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        { return View(); }

    }
}