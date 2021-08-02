using EnsekDAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnsekDAL.DAL
{
    public class ContentContext : DbContext
    {
        public ContentContext() : base("DefaultConnection")
        {
        }

        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
