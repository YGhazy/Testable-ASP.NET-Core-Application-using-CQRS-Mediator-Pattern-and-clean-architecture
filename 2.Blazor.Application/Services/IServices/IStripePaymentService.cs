using Blazor.Application.Common;
using Blazor.Application.DTOs;

namespace Blazor.Application.Services.IServices
{
    public interface IStripePaymentService
    {
        Task<ApiResponse<string>> Create(StripePaymentDTO paymentDTO);
    }
}
