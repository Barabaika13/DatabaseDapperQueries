namespace DatabaseDapperQueries
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var customersAll = DapperRepository.GetCustomersAll();
            foreach (var customer in customersAll)
            {
                PrintCustomer(customer);
            }

            var customerById = DapperRepository.GetCustomerById(5);
            PrintCustomer(customerById);

            var customers = DapperRepository.GetCustomersByLastNameParameter("%Wil%");
            foreach (var customer in customers)
            {
                PrintCustomer(customer);
            }

            var products = DapperRepository.GetAndOrderProductsByPrice(199.99m);
            foreach (var product in products)
            {
                PrintProduct(product);
            }

            var productByName = DapperRepository.GetProductByName("Tablet");
            PrintProduct(productByName);          

            var productsByQuantity = DapperRepository.GetAndOrderProductsByQuantity();
            foreach (var product in productsByQuantity)
            {
                PrintProduct(product);
            }

            var productBypriceAndQuantity = DapperRepository.GetProductByPriceAndQuantity(79.99m, 70);
            PrintProduct(productBypriceAndQuantity);

            var ordersByQuantity = DapperRepository.GetOrdersByQuantity(2);
            foreach (var order in ordersByQuantity)
            {
                PrintOrder(order);
            }

            var distinctCustomerId = DapperRepository.GetDistinctCustomerId();
            foreach (var customertId in distinctCustomerId)
            {
                PrintOrder(customertId);
            }

            var ordersByIdAndQuantity = DapperRepository.GetOrdersByIdAndQuantityMoreThan(3, 1);
            foreach (var order in ordersByIdAndQuantity)
            {
                PrintOrder(order);
            }
        }

        public static void PrintCustomer(Customer customer)
        {
            Console.WriteLine($"{customer.Id} {customer.FirstName} {customer.LastName} {customer.Age}");
        }

        public static void PrintProduct(Product product)
        {
            Console.WriteLine($"{product.Id} {product.Name} {product.Description} {product.StockQuantity} {product.Price}");
        }

        public static void PrintOrder(Order order)
        {
            Console.WriteLine($"{order.Id} {order.CustomerId} {order.ProductId} {order.Quantity}");
        }
    }
}