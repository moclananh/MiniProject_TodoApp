namespace Todo.Application.Services.TodoServices
{
    public interface ITodoService
    {
        Task<ApiResponse<PagingResult<TodoVm>>> GetAllTodos(FilterRequest request);
        Task<ApiResponse<PagingResult<TodoVm>>> GetTodosByUserId(Guid userId, FilterRequest request);
        Task<ApiResponse<TodoVm>> GetTodoById(int id);
        Task<ApiResponse<bool>> CreateTodo(CreateTodoRequest todoVm);
        Task<ApiResponse<bool>> UpdateTodo(int id, UpdateTodoRequest todoVm);
        Task<ApiResponse<bool>> StarUpdate(int id);
        Task<ApiResponse<bool>> DeleteTodo(int id);
    }

}
