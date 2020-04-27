using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ConsoleAPIClient.Clients;
using ConsoleAPIClient.Models;

namespace ConsoleAPIClient
{
    class MainClass
    {
        private static string basePath = "https://localhost:5003";

        public static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        private async static Task RunAsync()
        {
            var categoryClient = new CategoriesClient(basePath);
            var productsClient = new ProductsClient(basePath);

            var products = await productsClient.GetAllAsync();
            var categories = await categoryClient.GetAllAsync();

            Console.WriteLine("-----Products-------");
            foreach(var product in products)
            {
                Console.WriteLine(product.ToString());
            }
            Console.WriteLine("-----Categories-------");
            foreach (var category in categories)
            {
                Console.WriteLine(category.ToString());
            }

        }
    }
}
