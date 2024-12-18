using Microsoft.AspNetCore.Identity;
using TodoApp.Infrastructure.Dtos.UserDtos;

namespace Todo.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<LoginResponse> Authencate(LoginVm request);
        Task<ApiResponse> Register(RegisterVm request);
    }
}
