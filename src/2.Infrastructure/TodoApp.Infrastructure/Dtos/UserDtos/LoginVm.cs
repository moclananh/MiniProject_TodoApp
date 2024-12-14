using System.ComponentModel.DataAnnotations;

namespace TodoApp.Infrastructure.Dtos.UserDtos
{
    public class LoginVm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
