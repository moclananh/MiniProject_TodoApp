using TodoApp.Domain.Models.Entities;
using TodoApp.Infrastructure.Dtos.TodoDtos;

namespace TodoApp.Infrastructure.Repositories.TodosRepositories
{
    public interface ITodosRepository
    {
        Task<TodoResult> GetAllTodosAsync(FilterRequest request);
        Task<TodoResult> GetTodosByUserIdAsync(Guid userId, FilterRequest request);
        Task<Todo> GetTodoByIdAsync(int id);
        Task CreateTodoAsync(CreateTodoRequest todoVm);
        Task UpdateTodoAsync(int id, UpdateTodoRequest todoVm);
        Task DeleteTodoAsync(int id);
        Task<bool> ToggleTodoStarAsync(int id);
    }
}
