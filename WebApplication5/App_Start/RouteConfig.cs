using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Index", "{type}",
                new { controller = "Home", action = "Index" },
                new RouteValueDictionary
                {
                   {"type","trang-chu" }
                },
                namespaces: new[] { "WebApplication5.Controllers" });

            routes.MapRoute("Register", "{type}",
                new { controller = "Register", action = "Register" },
                new RouteValueDictionary
                {
                   {"type","dang-ky" }
                },
                namespaces: new[] { "WebApplication5.Controllers" });

            routes.MapRoute("Index1", "{type}",
             new { controller = "Login", action = "Index" },
             new RouteValueDictionary
             {
                   {"type","dang-nhap" }
             },
             namespaces: new[] { "WebApplication5.Controllers" });

            routes.MapRoute("About", "{type}",
                new { controller = "Home", action = "About" },
                new RouteValueDictionary
                {
                    {"type","ve-chung-toi" }
                },
                namespaces: new[] {"WebApplication5.Controllers"});
            routes.MapRoute("Contact", "{type}",
                new { controller = "Home", action = "Contact" },
                new RouteValueDictionary
                {
                    {"type","lien-he" }
                },
                namespaces: new[] { "WebApplication5.Controllers" });

            routes.MapRoute("News", "{type}",
                new { controller = "News", action = "Index" },
                new RouteValueDictionary
                {
                    {"type","tin-tuc" }
                },
                namespaces: new[] { "WebApplication5.Controllers" });

          

            routes.MapRoute("Service", "{type}/{meta}",
                    new { controller = "Service", action = "Index", meta = UrlParameter.Optional },
                    new RouteValueDictionary
                    {
                        {"type","dich-vu" }
                    },
                    namespaces: new[] { "WebApplication5.Controllers" });

            routes.MapRoute("Detail", "{type}/{meta}/{meta_detail}/{id}",
                    new { controller = "Service", action = "Detail", meta = UrlParameter.Optional },
                    new RouteValueDictionary
                    {
                        {"type","dich-vu" }
                    },
                    namespaces: new[] { "WebApplication5.Controllers" });

            routes.MapRoute("DetailNews", "{type}/{id}",
                    new { controller = "News", action = "DetailNews", meta = UrlParameter.Optional },
                    new RouteValueDictionary
                    {
                        {"type","tin-tuc" }
                    },
                    namespaces: new[] { "WebApplication5.Controllers" });

            //routes.MapRoute("Cart", "{type}",
            //        new { controller = "Cart", action = "AddItem", meta = UrlParameter.Optional },
            //        new RouteValueDictionary
            //        {
            //            {"type","gio-hang" }
            //        },
            //        namespaces: new[] { "WebApplication5.Controllers" });
            routes.MapRoute(
                name: "Add Cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart",action = "AddItem",id = UrlParameter.Optional },
                namespaces: new[] { "WebApplication5.Controllers" }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart",action = "Index",id = UrlParameter.Optional },
                namespaces: new[] { "WebApplication5.Controllers" }
            );

            routes.MapRoute(
                name: "Payment",
                url: "don-dat-dich-vu",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "WebApplication5.Controllers" }
            );
            routes.MapRoute(
                name: "SuccessPayment",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "WebApplication5.Controllers" }
            );
            routes.MapRoute(
                name: "Search",
                url: "tim-kiem",
                defaults: new { controller = "Service", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "WebApplication5.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home",action = "Index",id = UrlParameter.Optional },
                namespaces: new[] { "WebApplication5.Controllers" }
            );
        }
    }
}
