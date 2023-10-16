using Blazor.Application.DTOs;
using Features.Categories.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.Features.Products.Queries;

public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public CategoryDTO Category { get; set; }

        public ICollection<ProductPriceDTO> ProductPrices { get; set; }
    }

