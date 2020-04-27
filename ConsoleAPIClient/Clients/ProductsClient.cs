using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ConsoleAPIClient.Models;

namespace ConsoleAPIClient.Clients
{
    public class ProductsClient
    {
        private HttpClient _client;
        private readonly string _path = "api/products";

        public ProductsClient(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            IEnumerable<Product> products = null;
            HttpResponseMessage response = await _client.GetAsync($"{_path}?includeAll=true");
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
            }
            return products;
        }
    }
}
