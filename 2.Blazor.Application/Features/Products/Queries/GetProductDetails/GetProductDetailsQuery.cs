using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Products.Queries.GetProductDetails;

    public record GetProductDetailsQuery(int ID):IRequest<ProductDTO>;
    

