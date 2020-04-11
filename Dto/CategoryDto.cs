using System.ComponentModel.DataAnnotations;

namespace AppApi.Dto
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}