using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GateWay_ADO.DAL;
using EF_CodeFirst_Shop;

namespace EF_CodeFirst_Shop.BLL
{
    class BusinessLogic
    {
        //Получить список товаров заданной категории
        public List<Product> GetProductsByCategory(string categoryName)
        {
            List<Product> products = new List<Product>();
            using (CategoryContext categoryDB = new CategoryContext())
            {
                using (ProductContext productDB = new ProductContext())
                {
                    int catID = 0;
                    catID = categoryDB.Categories.Where(x => x.Name == categoryName).Select(c => c.Id).First();
                    products = productDB.Products.Where(x => x.CategoryId == catID).ToList();
                }
            }
            return products;
        }
        //Определить всех поставщиков товаров заданной категории
        public List<Provider> GetProvidersByCategory(string categoryName)
        {
            List<Provider> res = new List<Provider>();
            using (ProviderContext providerDB = new ProviderContext())
            {
                List<int> l1 = GetProductsByCategory(categoryName).Select(x => x.ProviderId).ToList();
                List<Provider> l2 = providerDB.Providers.ToList();
                foreach (Provider item in l2)
                {
                    if (l1.Any(c => c == item.Id))
                    {
                        res.Add(item);
                    }
                }
            }
            return res;
        }
        //Определить список товаров заданного поставщика
        public List<Product> GetProductsByProvider(string providerName)
        {
            List<Product> products = new List<Product>();
            using (ProductContext productDB = new ProductContext())
            {
                using (ProviderContext providerDB = new ProviderContext())
                {
                    Provider ourNeeded = providerDB.Providers.First(x => x.Name == providerName);
                    products = productDB.Products.Where(x => x.ProviderId == ourNeeded.Id).ToList();
                }
            }
            return products;
        }

        //Выполнять поиск по заданному критерию, например, поиск товаров с заданной ценой или
        //с максимальной/минимальной ценой, поиск поставщиков из заданного населенного пункта.
        public List<Product> GetProductsByPrice(double price)
        {
            List<Product> res = new List<Product>();
            using (ProductContext productDB = new ProductContext())
            {
                res = productDB.Products.Where(x => x.Price == price).ToList();

            }
            return res;
        }
        public List<Product> GetFiveCheapestProducts(string name)
        {
            List<Product> res = new List<Product>();
            using (ProductContext productDB = new ProductContext())
            {
                res = productDB.Products.Where(x => x.Name == name).OrderBy(x => x.Price).Take(5).ToList();

            }
            return res;
        }
    }
}
