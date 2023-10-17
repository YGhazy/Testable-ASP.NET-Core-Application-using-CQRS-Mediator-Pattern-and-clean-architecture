using Blazor.Application.DTOs;
using Features.Categories.Queries.GetCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Application.IRepository
{
    public interface ICategoryRepository
    {
        public Task Create(CategoryDTO objDTO);
        public Task<CategoryDTO> Update(CategoryDTO objDTO);
        public Task<int> Delete(int id);
        public Task<CategoryDTO> Get(int id);
        public Task<IEnumerable<CategoryDTO>> GetAll();
        Task<bool> IsCategoryUnique(string name);
    }
}
