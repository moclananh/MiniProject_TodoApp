
using TodoApp.Infrastructure;

namespace Todo.Application.Services.TodoServices
{
    public class TodoService : ITodoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TodoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PagingResult<TodoVm>>> GetAllTodos(FilterRequest request)
        {
            var result = await _unitOfWork.TodosRepository.GetAllTodosAsync(request);

            // Map the Todo entities to TodoVm ViewModels
            var todoVmList = _mapper.Map<List<TodoVm>>(result.Todos);

            // Create the paging result
            var pagingResult = new PagingResult<TodoVm>(request.PageNumber, request.PageSize, result.TotalCount, todoVmList);

            // Return the response wrapped in ApiResponse
            return new ApiResponse<PagingResult<TodoVm>>
            {
                Success = true,
                Message = "Todos fetched successfully.",
                Data = pagingResult
            };
        }

        public async Task<ApiResponse<PagingResult<TodoVm>>> GetTodosByUserId(Guid userId, FilterRequest request)
        {
            var result = await _unitOfWork.TodosRepository.GetTodosByUserIdAsync(userId, request);

            // Map the Todo entities to TodoVm ViewModels
            var todoVmList = _mapper.Map<List<TodoVm>>(result.Todos);

            // Create the paging result
            var pagingResult = new PagingResult<TodoVm>(request.PageNumber, request.PageSize, result.TotalCount, todoVmList);

            // Return the response wrapped in ApiResponse
            return new ApiResponse<PagingResult<TodoVm>>
            {
                Success = true,
                Message = "Todos fetched successfully.",
                Data = pagingResult
            };
        }

        public async Task<ApiResponse> GetTodoById(int id)
        {
            var todo = await _unitOfWork.TodosRepository.GetTodoByIdAsync(id);

            if (todo is null) throw new TodoNotFoundException(id);

            var todoVm = _mapper.Map<TodoVm>(todo);

            return new ApiResponse
            {
                Success = true,
                Message = "Todo fetched successfully.",
                Data = todoVm
            };
        }

        public async Task<ApiResponse> CreateTodo(CreateTodoRequest todoVm)
        {
            await _unitOfWork.TodosRepository.CreateTodoAsync(todoVm);

            return new ApiResponse
            {
                Success = true,
                Message = "Todo created successfully."
            };
        }

        public async Task<ApiResponse> UpdateTodo(int id, UpdateTodoRequest todoVm)
        {
            var todo = await _unitOfWork.TodosRepository.GetTodoByIdAsync(id);

            if (todo is null) throw new TodoNotFoundException(id);

            await _unitOfWork.TodosRepository.UpdateTodoAsync(id, todoVm);

            return new ApiResponse
            {
                Success = true,
                Message = "Todo updated successfully."
            };
        }

        public async Task<ApiResponse> StarUpdate(int id)
        {
            var todo = await _unitOfWork.TodosRepository.GetTodoByIdAsync(id);

            if (todo is null) throw new TodoNotFoundException(id);

            var starStatus = await _unitOfWork.TodosRepository.ToggleTodoStarAsync(id);

            return new ApiResponse
            {
                Success = true,
                Message = $"Todo star toggled successfully. New value: {starStatus}",
                Data = starStatus
            };
        }

        public async Task<ApiResponse> DeleteTodo(int id)
        {
            var todo = await _unitOfWork.TodosRepository.GetTodoByIdAsync(id);

            if (todo is null) throw new TodoNotFoundException(id);

            await _unitOfWork.TodosRepository.DeleteTodoAsync(id);

            return new ApiResponse
            {
                Success = true,
                Message = "Todo deleted successfully."
            };
        }
    }
}
