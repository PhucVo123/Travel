//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication5.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int id { get; set; }
        public Nullable<int> id_order { get; set; }
        public Nullable<int> id_pro { get; set; }
        public Nullable<int> quanity { get; set; }
        public Nullable<decimal> price { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}