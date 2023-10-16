using Features.Categories.Queries.GetCategories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Categories.Queries.GetCategoryDetails;

    public record GetCategoryDetailsQuery(int Id) : IRequest<CategoryDTO>;

