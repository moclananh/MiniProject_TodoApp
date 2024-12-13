
using TodoApp.Domain.Models;
using TodoApp.Infrastructure.Dtos.UserDtos;

namespace Todo.Application.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResponse> Authencate(LoginVm request);
        Task<ApiResponse> Register(RegisterVm request);
    }
}
