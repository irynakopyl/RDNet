using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GateWay_ADO.DAL;

namespace EF_CodeFirst_Shop
{
    class ProductContext : DbContext
    {
        public ProductContext()
            : base("DbConnection")
        { }

        public DbSet<Product> Products { get; set; }
    }
}
