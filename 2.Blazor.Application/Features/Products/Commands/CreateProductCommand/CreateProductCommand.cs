using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Products.Commands.CreateProductCommand;

public class CreateProductCommand:IRequest<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public string ImageUrl { get; set; }

    public int CategoryId { get; set; }
}
