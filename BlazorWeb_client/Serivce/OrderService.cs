using Newtonsoft.Json;
using System.Text;

using BlazorWeb_Client.Serivce.IService;
using Blazor.Application.DTOs;
using Blazor.Application.Common;

namespace BlazorWeb_Client.Serivce
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration=configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<OrderDTO> Create(StripePaymentDTO paymentDTO)
        {
            var content = JsonConvert.SerializeObject(paymentDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/order/create", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<ApiResponse<OrderDTO>>(responseResult);
                return result.Data;
            }
            return new OrderDTO();

        }

        public async Task<OrderDTO> Get(int orderHeaderId)
        {
            var response = await _httpClient.GetAsync($"/api/order/{orderHeaderId}");
            var content = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<ApiResponse<OrderDTO>>(content);
            if (response.IsSuccessStatusCode)
            {
                return order.Data;
            }
            else
            {
                //var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(order.Errors[0]);
            }
        }

        public async Task<IEnumerable<OrderDTO>> GetAll(string? userId=null)
        {
            var response = await _httpClient.GetAsync("/api/order");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<ApiResponse<IEnumerable<OrderDTO>>>(content);
               
                return orders.Data;
            }

            return new List<OrderDTO>();
        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(OrderHeaderDTO orderHeader)
        {
            var content = JsonConvert.SerializeObject(orderHeader);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/order/paymentsuccessful", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<ApiResponse<OrderHeaderDTO>>(responseResult);
            if (response.IsSuccessStatusCode)
            {
                return result.Data;
            }
            //var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
            throw new Exception(result.Errors[0]);
        }
    }
}
