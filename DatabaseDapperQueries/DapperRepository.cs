using Dapper;
using Npgsql;

namespace DatabaseDapperQueries
{
    internal class DapperRepository
    {
        public static IEnumerable<Customer> GetCustomersAll()
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = "SELECT id, firstname, lastname, age FROM customers";
                return conn.Query<Customer>(sql);
            }
        }

        public static IEnumerable<Customer> GetCustomersByLastNameParameter(string pattern)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {               
                string query = $"SELECT id, firstname, lastname, age FROM Customers WHERE lastname LIKE @pattern";
                var customers = conn.Query<Customer>(query, new { pattern });
                return customers;

            }
        }

        public static Customer GetCustomerById(int id)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string sql = $"SELECT id, firstname, lastname, age FROM customers WHERE id = @id";
                var customer = conn.QueryFirstOrDefault<Customer>(sql, new {id});
                if (customer == null)
                {
                    Console.WriteLine("Customer not found");
                    return null;
                }
                else
                {
                    return customer;                    
                }
            }
        }

        public static IEnumerable<Product> GetAndOrderProductsByPrice(decimal price)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string query = $"SELECT id, name, description, stockquantity, price FROM products WHERE price > @price ORDER BY price";
                var products = conn.Query<Product>(query, new { price });
                return products;

            }
        }

        public static Product GetProductByName(string name)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string query = $"SELECT id, name, description, stockquantity, price FROM products WHERE name LIKE @name";
                var product = conn.QueryFirstOrDefault<Product>(query, new { name });
                if (product == null)
                {
                    Console.WriteLine("Product not found");
                    return null;
                }
                else
                {
                    return product;
                }
            }
        }

        public static IEnumerable<Product> GetAndOrderProductsByQuantity()
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string query = $"SELECT id, name, description, stockquantity, price FROM products ORDER BY stockquantity desc";
                return conn.Query<Product>(query);

            }
        }

        public static Product GetProductByPriceAndQuantity(decimal price, int stockQuantity)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string query = $"SELECT id, name, description, stockquantity, price FROM products WHERE price = @price AND stockQuantity = @stockquantity";
                var product = conn.QueryFirstOrDefault<Product>(query, new { price, stockQuantity });                
                if (product == null)
                {
                    Console.WriteLine("Product not found");
                    return null;
                }
                else
                {
                    return product;
                }
            }
        }

        public static IEnumerable<Order> GetOrdersByQuantity(int quantity)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string query = $"SELECT id, customerId, productId, quantity FROM orders WHERE quantity = @quantity ";
                return conn.Query<Order>(query, new {quantity});
            }
        }

        public static IEnumerable<Order> GetDistinctCustomerId()
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string query = $"SELECT distinct customerid FROM orders";
                return conn.Query<Order>(query);
            }
        }

        public static IEnumerable<Order> GetOrdersByIdAndQuantityMoreThan(int id, int quantity)
        {
            using (var conn = new NpgsqlConnection(Config.SqlConnectionString))
            {
                string query = $"SELECT id, customerId, productId, quantity FROM orders WHERE id > @id AND quantity > @quantity ORDER BY id";
                return conn.Query<Order>(query, new {id, quantity });
            }
        }
    }
}
