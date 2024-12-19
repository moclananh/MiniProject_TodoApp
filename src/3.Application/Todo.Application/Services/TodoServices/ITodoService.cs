namespace Todo.Application.Services.TodoServices
{
    public interface ITodoService
    {
        Task<ApiResponse<PagingResult<TodoVm>>> GetAllTodos(FilterRequest request);
        Task<ApiResponse<PagingResult<TodoVm>>> GetTodosByUserId(Guid userId, FilterRequest request);
        Task<ApiResponse> GetTodoById(int id);
        Task<ApiResponse> CreateTodo(CreateTodoRequest todoVm);
        Task<ApiResponse> UpdateTodo(int id, UpdateTodoRequest todoVm);
        Task<ApiResponse> StarUpdate(int id);
        Task<ApiResponse> DeleteTodo(int id);
    }

}
