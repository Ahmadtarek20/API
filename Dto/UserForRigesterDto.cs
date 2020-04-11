using System.ComponentModel.DataAnnotations;

namespace AppApi.Dto
{
    public class UserForRigesterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="THEM 8 BYT")]
        public string Password { get; set; }
    }
}