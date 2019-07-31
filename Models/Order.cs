using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public List<Product> products { get; set; }
    }
}
