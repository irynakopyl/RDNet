using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GateWay_ADO.BLL;
using System.Data.SqlClient;
using GateWay_ADO.DAL;

namespace GateWay_ADO
{
    class Program
    {
        static void Main(string[] args)
        {
            LogicProvider logic = new LogicProvider();
            Console.WriteLine("Что вы хотели бы сделать?");
            Console.WriteLine(" 1 - Получить список товаров заданной категории");
            Console.WriteLine(" 2 - Определить всех поставщиков товаров заданной категории");
            Console.WriteLine(" 3 -  Определить список товаров заданного поставщика");
            Console.WriteLine(" 4 -  Поиск товаров с заданной ценой");
            Console.WriteLine(" 5 -  Поиск товара с минимальной ценой");

            Console.WriteLine("Выберите цифру:");
            int selection = int.Parse(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    string category;
                    Console.WriteLine(" Введите категорию продуктов ");
                    category = Console.ReadLine();
                    var products = logic.GetProductsByCategory(category);
                    foreach (var p in products)
                    {
                        Console.WriteLine(p.ToString());
                    }
                    break;
                case 2:
                    Console.WriteLine(" Введите категорию продуктов ");
                    category = Console.ReadLine();
                    var providers = logic.GetProvidersByCategory(category);
                    foreach (var p in providers)
                    {
                        Console.WriteLine(p.ToString());
                    }
                    break;
                case 3:
                    Console.WriteLine(" Введите поставщика ");
                    category = Console.ReadLine();
                    var prods = logic.GetProductsByProvider(category);
                    foreach (var p in prods)
                    {
                        Console.WriteLine(p.ToString());
                    }
                    break;
                case 4:
                    Console.WriteLine(" Введите цену ");
                    double price = double.Parse(Console.ReadLine());
                    var pros = logic.GetProductsByPrice(price);
                    foreach (var p in pros)
                    {
                        Console.WriteLine(p.ToString());
                    }
                    break;
                case 5:
                    Console.WriteLine(" Введите название продукта: ");
                    string name = Console.ReadLine();
                    var prod = logic.GetFiveCheapestProducts(name);
                    foreach (var p in prod)
                    {
                        Console.WriteLine(p.ToString());
                    }
                    break;
                default:
                    Console.WriteLine("Вы нажали неизвестную букву");
                    break;
            }


            
            
        }
    }
   
}
