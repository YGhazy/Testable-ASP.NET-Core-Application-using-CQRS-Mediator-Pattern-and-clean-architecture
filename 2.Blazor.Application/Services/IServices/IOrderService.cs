using Blazor.Application.Common;
using Blazor.Application.DTOs;

namespace Blazor.Application.Services.IServices
{
    public interface IOrderService
    {
        public  Task<ApiResponse<IEnumerable<OrderDTO>>> GetAll();
        public  Task<ApiResponse<OrderDTO>> Get(int? orderHeaderId);
        public  Task<ApiResponse<OrderDTO>> Create(StripePaymentDTO paymentDTO);
        Task<ApiResponse<OrderHeaderDTO>> PaymentSuccessful(OrderHeaderDTO orderHeaderDTO);

    }
}
