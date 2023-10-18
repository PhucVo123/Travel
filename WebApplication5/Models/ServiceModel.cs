using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class ServiceModel
    {
        public int id { get; set; }
        public string position_detail { get; set; }
        public string facilities { get; set; }
        public string depart { get; set; }
        public string transit { get; set; }
        public string starting { get; set; }
        public string descDetail { get; set; }
        public string scheduleDetail { get; set; }
        public Nullable<int> ProductID { get; set; }

        public Nullable<int> CategoryID { get; set; }
        public string namePackage { get; set; }
        public string position { get; set; }
        public string dateStay { get; set; }
        public string numberOfCustomer { get; set; }
        public string star { get; set; }
        public string price { get; set; }
        public string area { get; set; }
        public string img { get; set; }
        public string meta_detail { get; set; }
        public string meta { get; set; }
        public Nullable<bool> hide { get; set; }
        public Nullable<int> order { get; set; }
        public Nullable<int> order_detail { get; set; }

        public Nullable<System.DateTime> datebegin { get; set; }
    }
}