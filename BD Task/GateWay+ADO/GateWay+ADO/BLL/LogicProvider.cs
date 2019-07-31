using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay_ADO.DAL;
namespace GateWay_ADO.BLL
{
    class LogicProvider
    {
        string connString = "Data Source=(localDB)\\MSSQLLocalDB;Initial Catalog=Shop;Integrated Security=True";
        //Получить список товаров заданной категории
        public List<Product> GetProductsByCategory(string categoryName)
        {
            List<Product> products = new List<Product>();
            CategoryGateWay categoryGateWay = new CategoryGateWay(connString);
            ProductGateWay productGateWay = new ProductGateWay(connString);
            int catID = 0;
            catID = categoryGateWay.GetAll().Where(x => x.Name == categoryName).Select(c => c.Id).First();
            products = productGateWay.GetAll().Where(x => x.CategoryId == catID).ToList();
            return products;
        }
        //Определить всех поставщиков товаров заданной категории
        public List<Provider> GetProvidersByCategory(string categoryName)
        {
            ProviderGateWay providerGateWay = new ProviderGateWay(connString);
            List<Provider> providers = new List<Provider>();

            List<int> l1 = GetProductsByCategory(categoryName).Select(x => x.ProviderId).ToList();
            List<Provider> l2 = providerGateWay.GetAll().ToList();
            List<Provider> res = new List<Provider>();

            foreach (Provider item in l2)
            {
                if (l1.Any(c => c == item.Id))
                {
                    res.Add(item);
                }
            }

            return res;
        }
        //Определить список товаров заданного поставщика
        public List<Product> GetProductsByProvider(string providerName)
        {
            ProductGateWay productGateWay = new ProductGateWay(connString);
            ProviderGateWay providerGateWay = new ProviderGateWay(connString);
            Provider ourNeeded = providerGateWay.GetAll().First(x=>x.Name==providerName);
            return productGateWay.GetAll().Where(x => x.ProviderId == ourNeeded.Id).ToList();
        }

        //Выполнять поиск по заданному критерию, например, поиск товаров с заданной ценой или
        //с максимальной/минимальной ценой, поиск поставщиков из заданного населенного пункта.
        public List<Product> GetProductsByPrice(double price)
        {
            ProductGateWay productGateWay = new ProductGateWay(connString);
            return productGateWay.GetAll().Where(x => x.Price == price).ToList();
        }
        public List<Product> GetFiveCheapestProducts(string name)
        {
            ProductGateWay productGateWay = new ProductGateWay(connString);
            return productGateWay.GetAll().Where(x=>x.Name==name).OrderBy(x=>x.Price).Take(5).ToList();
        }





    }
}
