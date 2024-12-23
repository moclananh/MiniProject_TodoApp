using TodoApp.Domain.Models.Entities;

namespace TodoApp.Infrastructure.Dtos.UserDtos
{
    public class AuthenticateResponse
    {
        public int Result { get; set; }
        public User User { get; set; }
    }
}
