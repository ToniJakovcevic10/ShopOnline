﻿using ShopOnlineModels.Dtos;
using ShopOnlineWeb.Services.Contracts;
using System.Net.Http;
using System.Net.Http.Json;

namespace ShopOnlineWeb.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDto>> getItems()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}