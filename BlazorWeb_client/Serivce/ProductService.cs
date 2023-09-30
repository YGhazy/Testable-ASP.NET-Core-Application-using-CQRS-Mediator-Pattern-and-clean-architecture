using Newtonsoft.Json;
using BlazorWeb_Client.Serivce.IService;
using Blazor.Application.DTOs;
using Blazor.Application.Common;

namespace BlazorWeb_Client.Serivce
{
    public class ProductService : IProductService
    {

        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration=configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<ProductDTO> Get(int productId)
        {
            var response = await _httpClient.GetAsync($"/api/product/{productId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ApiResponse<ProductDTO>>(content).Data;
                product.ImageUrl=BaseServerUrl+product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<ProductDTO>>>(content);
                foreach(var prod in products.Data)
                {
                    prod.ImageUrl=BaseServerUrl+prod.ImageUrl;
                }
                return products.Data;
            }

            return new List<ProductDTO>();
        }
    }
}
