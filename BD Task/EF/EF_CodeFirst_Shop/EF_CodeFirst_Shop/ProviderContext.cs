using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay_ADO.DAL;
using System.Data.Entity;


namespace EF_CodeFirst_Shop
{
    public class ProviderContext : DbContext
    {
        public ProviderContext()
            : base("DbConnection")
        { }

        public DbSet<Provider> Providers { get; set; }
    }
}
