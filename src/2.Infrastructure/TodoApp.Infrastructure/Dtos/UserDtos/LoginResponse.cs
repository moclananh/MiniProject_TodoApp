
using TodoApp.Domain.Models;

namespace TodoApp.Infrastructure.Dtos.UserDtos
{
    public class LoginResponse : ApiResponse<string>
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
