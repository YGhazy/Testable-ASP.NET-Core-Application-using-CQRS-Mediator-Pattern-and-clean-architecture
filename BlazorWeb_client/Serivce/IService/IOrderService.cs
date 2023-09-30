using Blazor.Application.DTOs;

namespace BlazorWeb_Client.Serivce.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId);
        public Task<OrderDTO> Get(int orderId);

        public Task<OrderDTO> Create(StripePaymentDTO paymentDTO);

        public Task<OrderHeaderDTO> MarkPaymentSuccessful(OrderHeaderDTO orderHeader);
    }
}
