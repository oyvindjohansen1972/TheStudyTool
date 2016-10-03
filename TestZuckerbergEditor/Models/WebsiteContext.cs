using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace TestZuckerbergEditor.Models
{
    public class WebsiteContext : DbContext
    {
        public DbSet<Wall> Walls { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public WebsiteContext()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //   // modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();         
        //}
    }
}