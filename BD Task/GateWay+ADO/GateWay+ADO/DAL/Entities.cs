using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GateWay_ADO.DAL
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int ProviderId { get; set; }
        public override string  ToString()
        {
            return $" I am a Product Name:{Name},Price:{Price} ";
        }
    }
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"I am a provider MY Name:{Name}";
        }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"I am a category MY Name:{Name}";
        }
    }
}
