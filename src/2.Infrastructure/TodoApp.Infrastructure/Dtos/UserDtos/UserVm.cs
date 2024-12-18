
namespace TodoApp.Infrastructure.Dtos.UserDtos
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
