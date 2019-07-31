using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
    }
    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public List<Product> products { get; set; }
       
    }
    public class OrderContext : DbContext
    {
        public DbSet<Product> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}