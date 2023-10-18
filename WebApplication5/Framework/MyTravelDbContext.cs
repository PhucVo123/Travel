using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebApplication5.Framework
{
    public partial class MyTravelDbContext : DbContext
    {
        public MyTravelDbContext()
            : base("name=MyTravelDbContext")
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.pwd)
                .IsUnicode(false);
        }
    }
}
