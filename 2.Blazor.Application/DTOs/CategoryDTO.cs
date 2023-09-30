using System.ComponentModel.DataAnnotations;

namespace Blazor.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter name..")]
        public string Name { get; set; }
    }
}
