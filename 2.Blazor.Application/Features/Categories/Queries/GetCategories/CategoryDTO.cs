using System.ComponentModel.DataAnnotations;

namespace Features.Categories.Queries.GetCategories
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
