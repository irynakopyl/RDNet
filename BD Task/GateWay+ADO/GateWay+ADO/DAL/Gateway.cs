using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace GateWay_ADO.DAL
{
    interface IDataGateway<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int? id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
    public class ProductGateWay : IDataGateway<Product>
    {
        private string connectionString;

        public ProductGateWay(string connection)
        {
            connectionString = connection;
        }


        public IEnumerable<Product> GetAll()
        {
            string query = "SELECT * FROM Products";
            ICollection<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    double price = reader.GetFloat(2);
                    int categoryId = reader.GetInt32(3);
                    int providers = reader.GetInt32(4);
                    products.Add(new Product
                    {
                        Id = id,
                        Name = name,
                        Price = price,
                        CategoryId = categoryId,
                        ProviderId = providers
                    });

                }
            }
            return products;
        }

        public Product Get(int? id)
        {
            string query = "SELECT * FROM Products WHERE Products.Id = @id";
            Product prod = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prod = new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Price = reader.GetDouble(2),
                        CategoryId = reader.GetInt32(3),
                        ProviderId = reader.GetInt32(4)
                    };

                }

            }
            return prod;
        }
        public void Create(Product item)
        {
            string query = "INSERT INTO Products VALUES (@id, @name, @price, @category, @provider)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", item.Id);
                command.Parameters.Add(idParam);
                SqlParameter nameParam = new SqlParameter("@name", item.Name);
                command.Parameters.Add(nameParam);
                SqlParameter priceParam = new SqlParameter("@price", item.Price);
                command.Parameters.Add(priceParam);
                SqlParameter catParam = new SqlParameter("@category", item.CategoryId);
                command.Parameters.Add(catParam);
                SqlParameter provParam = new SqlParameter("@provider", item.ProviderId);
                command.Parameters.Add(catParam);
                SqlDataReader reader = command.ExecuteReader();
            }

        }
        public void Update(Product item)
        {
            Delete(item.Id);
            Create(item);
        }

        public void Delete(int item)
        {
            string query = "DELETE FROM Products WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", item);
                SqlDataReader reader = command.ExecuteReader();
            }

        }

        
    }

    public class CategoryGateWay : IDataGateway<Category>
    {
        private string connectionString;

        public CategoryGateWay(string connection)
        {
            connectionString = connection;
        }


        public IEnumerable<Category> GetAll()
        {
            string query = "SELECT * FROM [dbo].Categories";
            ICollection<Category> categories = new List<Category>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    categories.Add(
                        new Category { Id = id, Name = name });
                }
            }
            return categories;
        }

        public Category Get(int? id)
        {
            string query = "SELECT * FROM Categories WHERE Categories.Id = @id";
            Category prod = new Category();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prod = new Category
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                    };

                }

            }
            return prod;
        }
        public void Create(Category item)
        {
            string query = "INSERT INTO Categories VALUES @id, @name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", item.Id);
                command.Parameters.Add(idParam);
                SqlParameter nameParam = new SqlParameter("@name", item.Name);
                command.Parameters.Add(nameParam);
                SqlDataReader reader = command.ExecuteReader();
            }

        }
        public void Update(Category item)
        {
            Delete(item.Id);
            Create(item);
        }

        public void Delete(int item)
        {
            string query = "DELETE FROM Categories WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", item);
                SqlDataReader reader = command.ExecuteReader();
            }

        }
    }
    public class ProviderGateWay : IDataGateway<Provider>
    {
        private string connectionString;

        public ProviderGateWay(string connection)
        {
            connectionString = connection;
        }


        public IEnumerable<Provider> GetAll()
        {
            string query = "SELECT * FROM Providers";
            ICollection<Provider> providers = new List<Provider>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    providers.Add(new Provider { Id = id, Name = name });
                }
            }
            return providers;
        }

        public Provider Get(int? id)
        {
            string query = "SELECT * FROM Providers WHERE Providers.Id = @id";
            Provider prod = new Provider();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", id);
                command.Parameters.Add(idParam);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prod = new Provider
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                    };

                }

            }
            return prod;
        }
        public void Create(Provider item)
        {
            string query = "INSERT INTO Providers VALUES @id, @name";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", item.Id);
                command.Parameters.Add(idParam);
                SqlParameter nameParam = new SqlParameter("@name", item.Name);
                command.Parameters.Add(nameParam);
                SqlDataReader reader = command.ExecuteReader();
            }

        }
        public void Update(Provider item)
        {
            Delete(item.Id);
            Create(item);
        }

        public void Delete(int item)
        {
            string query = "DELETE FROM Providers WHERE Id = @id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);

                command.Connection.Open();
                SqlParameter idParam = new SqlParameter("@id", item);
                SqlDataReader reader = command.ExecuteReader();
            }

        }
    }

   
}
