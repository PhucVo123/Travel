using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication5.Framework;
namespace WebApplication5
{
    public class AccountModel
    {
        private MyTravelDbContext db = null;

        public AccountModel()
        {
            db = new MyTravelDbContext();
        }
        public bool Login( string username, string password)
        {
            object[] sqlParams = 
            {
                new SqlParameter("@username", username),
                new SqlParameter("@pwd", password)
            };
            var res = db.Database.SqlQuery<bool>("Travel_Account_Login @username, @pwd", sqlParams).SingleOrDefault();
            return res;
        }


    }
}