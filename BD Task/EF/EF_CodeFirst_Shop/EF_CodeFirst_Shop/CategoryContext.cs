using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay_ADO.DAL;
using System.Data.Entity;


namespace EF_CodeFirst_Shop
{
    public class CategoryContext : DbContext
    {
        public CategoryContext()
            : base("DbConnection")
        { }

        public DbSet<Category> Categories { get; set; }
    }
}

