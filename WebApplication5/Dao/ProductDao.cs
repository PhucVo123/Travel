using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.Dao
{
    public class ProductDao
    {
        TravelDB db = new TravelDB();
        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x=>x.position.Contains(keyword)).Select(x=>x.position).ToList();
        }
    }
}