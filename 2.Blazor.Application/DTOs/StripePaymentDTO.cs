using Blazor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.DTOs
{
    public class StripePaymentDTO
    {
        public StripePaymentDTO()
        {
            SuccessUrl = "OrderConfirmation";
            CancelUrl = "Summary";
        }
        public OrderDTO Order { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
    }
}
