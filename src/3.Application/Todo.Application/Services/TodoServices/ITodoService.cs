

using TodoApp.Domain.Models;
using TodoApp.Infrastructure.Dtos.TodoDtos;
using TodoApp.Infrastructure.Pagination;

namespace Todo.Application.Services.TodoServices
{
    public interface ITodoService
    {
        Task<ApiResponse> GetAllTodos(PagingRequest pagingRequest, string? keyword);
        Task<ApiResponse> GetTodoById(int id);
        Task<ApiResponse> CreateTodo(CreateTodoRequest todoVm);
        Task<ApiResponse> UpdateTodo(int id, UpdateTodoRequest todoVm);
        Task<ApiResponse> DeleteTodo(int id);
    }

}
