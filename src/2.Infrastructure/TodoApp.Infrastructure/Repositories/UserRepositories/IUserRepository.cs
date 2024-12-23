
using TodoApp.Infrastructure.Dtos.UserDtos;

namespace TodoApp.Infrastructure.Repositories.UserRepositories
{
    public interface IUserRepository
    {
        Task<AuthenticateResponse> AuthenticateUser(LoginVm request);
        Task<int> RegisterUser(RegisterVm request);

    }
}
