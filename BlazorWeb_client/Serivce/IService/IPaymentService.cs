using Blazor.Application.DTOs;

namespace BlazorWeb_Client.Serivce.IService
{
    public interface IPaymentService
    {
        public Task<string> Checkout(StripePaymentDTO model);

    }
}
