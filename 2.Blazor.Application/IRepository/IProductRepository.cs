using Blazor.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.IRepository
{
    public interface IProductRepository
    {
        public Task<ProductDTO> Create(ProductDTO objDTO);
        public Task<ProductDTO> Update(ProductDTO objDTO);
        public Task<int> Delete(int id);
        public Task<ProductDTO> Get(int id);
        public Task<IEnumerable<ProductDTO>> GetAll();
    }
}
