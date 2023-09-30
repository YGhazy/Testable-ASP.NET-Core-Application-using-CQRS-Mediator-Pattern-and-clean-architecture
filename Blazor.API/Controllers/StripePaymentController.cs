using Blazor.API.Common;
using Blazor.Application.DTOs;
using Blazor.Application.Services;
using Blazor.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Blazor.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StripePaymentController : BaseResultHandlerController<IStripePaymentService>
    {
        private readonly IStripePaymentService _stripePaymentService;
    public StripePaymentController(IStripePaymentService stripePaymentService) : base(stripePaymentService)
    {
            _stripePaymentService = stripePaymentService;
    }


        [HttpPost]
        [Authorize]
        [ActionName("Create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            return await AddItemResponseHandler(async () => await service.Create(paymentDTO));

        }
    }
}
