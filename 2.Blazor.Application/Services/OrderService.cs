using AutoMapper;
using Blazor.Application.Common;
using Blazor.Application.DTOs;
using Blazor.Application.IRepository;
using Blazor.Application.Services.IServices;
using Blazor.Domain.Common;
using Microsoft.AspNetCore.Http;
using Stripe.Checkout;

namespace Blazor.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IMapper mapper, IUnitOfWork unitOfWork, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<IEnumerable<OrderDTO>>> GetAll()
        {
            ApiResponse<IEnumerable<OrderDTO>> result = new ApiResponse<IEnumerable<OrderDTO>>();
            try
            {
                result.Succeeded = true;
                result.Data = await _orderRepository.GetAll();
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

        public async Task<ApiResponse<OrderDTO>> Get(int? orderHeaderId)
        {
            ApiResponse<OrderDTO> result = new ApiResponse<OrderDTO>();
            try
            {
                if (orderHeaderId==null || orderHeaderId==0)
                {

                    result.Succeeded = false;
                    result.Errors.Add("Invalid Id");
                    //StatusCode=StatusCodes.Status400BadRequest
                    return result;
                }

                var orderHeader = await _orderRepository.Get(orderHeaderId.Value);
                if (orderHeader==null)
                {
                    result.Succeeded = false;
                    result.Errors.Add("Invalid Id");
                    //StatusCode=StatusCodes.Status400BadRequest
                    return result;
                }

                result.Succeeded = true;
                result.Data = orderHeader;
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

        public async Task<ApiResponse<OrderDTO>> Create(StripePaymentDTO paymentDTO)
        {
            ApiResponse<OrderDTO> result = new ApiResponse<OrderDTO>();
            try
            {
                paymentDTO.Order.OrderHeader.OrderDate=DateTime.Now;
                result.Succeeded = true;
                result.Data = await _orderRepository.Create(paymentDTO.Order);
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

        public async Task<ApiResponse<OrderHeaderDTO>> PaymentSuccessful(OrderHeaderDTO orderHeaderDTO)
        {
            ApiResponse<OrderHeaderDTO> result = new ApiResponse<OrderHeaderDTO>();
            try
            {
                //var service = new SessionService();
                //var sessionDetails = service.Get(orderHeaderDTO.SessionId);
                //if (sessionDetails.PaymentStatus =="paid")
                //{
                    result.Succeeded = true;
                    result.Data =await _orderRepository.MarkPaymentSuccessful(orderHeaderDTO.Id);


                    //await _emailSender.SendEmailAsync(orderHeaderDTO.Email, " Order Confirmation",
                    //    "New Order has been created :" + orderHeaderDTO.Id);
                    if (result.Data==null)
                    {
                        result.Succeeded = false;
                        result.Errors.Add("Can not mark payment as successful");
                        return result;

                    }
                    return result;
                //}
                //result.Succeeded = false;
                //result.Errors.Add("Failed");
                //return result;
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
