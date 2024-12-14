

using System.ComponentModel.DataAnnotations;

namespace TodoApp.Infrastructure.Dtos.UserDtos
{
    public class RegisterVm
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
