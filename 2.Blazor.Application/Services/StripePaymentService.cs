using AutoMapper;
using Blazor.Application.Common;
using Blazor.Application.DTOs;
using Blazor.Application.IRepository;
using Blazor.Application.Services.IServices;
using Blazor.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace Blazor.Application.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IConfiguration _configuration;

        public StripePaymentService(IMapper mapper, IUnitOfWork unitOfWork, IOrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ApiResponse<string>> Create(StripePaymentDTO paymentDTO)
        {
            ApiResponse<string> result = new ApiResponse<string>();

            try
            {
                var domain = _configuration.GetRequiredSection("Blazor_Client_URL").Value;

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain+paymentDTO.SuccessUrl,
                    CancelUrl = domain+paymentDTO.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };


                foreach (var item in paymentDTO.Order.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price*100), //20.00 -> 2000
                            Currency="usd",
                            ProductData= new SessionLineItemPriceDataProductDataOptions
                            {
                                Name= item.Product.Name
                            }
                        },
                        Quantity= item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);
                result.Succeeded = true;
                result.Data = session.Id+";"+session.PaymentIntentId;
                return result;
            }
            catch (Exception ex)
            {
                result.Succeeded = false;
                result.Errors.Add(ex.Message);
                result.ErrorType = ErrorType.SystemError;
                return result;
            }
        }
    }
}
