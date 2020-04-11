using System.ComponentModel.DataAnnotations;

namespace AppApi.Dto
{
    public class UserForLoginDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}