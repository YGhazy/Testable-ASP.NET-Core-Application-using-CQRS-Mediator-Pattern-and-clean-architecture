using Blazor.API.Common;
using Blazor.Application.DTOs;
using Blazor.Application.Services.IServices;
using Blazor.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Blazor.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : BaseResultHandlerController<IOrderService>
    {
        private readonly IOrderService _orderService;
        public OrderController( IOrderService orderService) : base(orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return await GetResponseHandler(async () => await service.GetAll());
        }

        [HttpGet("{orderHeaderId}")]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            return await GetResponseHandler(async () => await service.Get(orderHeaderId));

        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            return await AddItemResponseHandler(async () => await service.Create(paymentDTO));

        }

        [HttpPost]
        [ActionName("paymentsuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDTO orderHeaderDTO)
        {
            return await AddItemResponseHandler(async () => await service.PaymentSuccessful(orderHeaderDTO));

        }
    }
}
