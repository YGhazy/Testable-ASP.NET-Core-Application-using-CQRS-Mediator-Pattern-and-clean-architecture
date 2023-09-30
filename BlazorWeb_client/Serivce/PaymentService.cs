using Newtonsoft.Json;
using System.Text;
using BlazorWeb_Client.Serivce.IService;
using Blazor.Application.DTOs;
using Blazor.Application.Common;

namespace BlazorWeb_Client.Serivce
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Checkout(StripePaymentDTO model)
        {
            try
            {
                var content = JsonConvert.SerializeObject(model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/stripepayment/create", bodyContent);
                string responseResult = response.Content.ReadAsStringAsync().Result;
                 var result = JsonConvert.DeserializeObject<ApiResponse<string>>(responseResult);
                if (response.IsSuccessStatusCode)
                {
                    return result.Data;
                }
                else
                {
                    //var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
                    throw new Exception(result.Errors[0]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


       
    }
}
