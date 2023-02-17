using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NotUygulamasıUnited.Models
{
    public class NotContext : DbContext
    {
        public DbSet<Not> Notlar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Not>()
                .HasOptional(n => n.UstNot)
                .WithMany(n => n.AltNotlar)
                .HasForeignKey(n => n.UstNotId);
        }
    }

}