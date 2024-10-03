using System.ComponentModel.DataAnnotations;

namespace ThuvienMvc.Dtos.UserDtos
{
    public class SignInUserDto
    {
        public int IdSign {  get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
