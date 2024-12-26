
using Microsoft.AspNetCore.Http;
using TodoApp.BuildingBlock.Utilities;
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
            try
            {
                var result = await _unitOfWork.TodosRepository.GetAllTodosAsync(request);

                // Map the Todo entities to TodoVm ViewModels
                var todoVmList = _mapper.Map<List<TodoVm>>(result.Todos);

                // Create the paging result
                var pagingResult = new PagingResult<TodoVm>(request.PageNumber, request.PageSize, result.TotalCount, todoVmList);

                // Return the response wrapped in ApiResponse
                return new ApiResponse<PagingResult<TodoVm>>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.TodoMessageResponses.TodoFetched,
                    Data = pagingResult
                };
            }
            catch (Exception)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError);
            }
        }

        public async Task<ApiResponse<PagingResult<TodoVm>>> GetTodosByUserId(Guid userId, FilterRequest request)
        {
            try
            {
                var result = await _unitOfWork.TodosRepository.GetTodosByUserIdAsync(userId, request);

                // Map the Todo entities to TodoVm ViewModels
                var todoVmList = _mapper.Map<List<TodoVm>>(result.Todos);

                // Create the paging result
                var pagingResult = new PagingResult<TodoVm>(request.PageNumber, request.PageSize, result.TotalCount, todoVmList);

                // Return the response wrapped in ApiResponse
                return new ApiResponse<PagingResult<TodoVm>>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Message = SystemConstants.TodoMessageResponses.TodoFetched,
                    Data = pagingResult
                };
            }
            catch (Exception)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError);
            }
        }

        public async Task<ApiResponse<TodoVm>> GetTodoById(int id)
        {
            var todo = await _unitOfWork.TodosRepository.GetTodoByIdAsync(id);

            if (todo is null) throw new TodoNotFoundException(id);

            var todoVm = _mapper.Map<TodoVm>(todo);

            return new ApiResponse<TodoVm>
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = SystemConstants.TodoMessageResponses.TodoFetched,
                Data = todoVm
            };
        }

        public async Task<ApiResponse<bool>> CreateTodo(CreateTodoRequest todoVm)
        {
            try
            {
                await _unitOfWork.TodosRepository.CreateTodoAsync(todoVm);

                return new ApiResponse<bool>
                {
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status201Created,
                    Message = SystemConstants.TodoMessageResponses.TodoCreated
                };
            }
            catch (Exception)
            {
                throw new InternalServerException(SystemConstants.InternalMessageResponses.InternalMessageError);
            }
        }

        public async Task<ApiResponse<bool>> UpdateTodo(int id, UpdateTodoRequest todoVm)
        {
            await _unitOfWork.BeginTransactionAsync();  // Begin transaction with pessimistic locking

            var todo = await _unitOfWork.TodosRepository.GetTodoByIdAsync(id);

            if (todo is null) throw new TodoNotFoundException(id);

            await _unitOfWork.TodosRepository.UpdateTodoAsync(id, todoVm);

            // Commit the transaction if everything is successful
            await _unitOfWork.CommitTransactionAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = SystemConstants.TodoMessageResponses.TodoUpdated
            };
        }

        public async Task<ApiResponse<bool>> StarUpdate(int id)
        {
            var todo = await _unitOfWork.TodosRepository.GetTodoByIdAsync(id);

            if (todo is null) throw new TodoNotFoundException(id);

            var starStatus = await _unitOfWork.TodosRepository.ToggleTodoStarAsync(id);

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = SystemConstants.TodoMessageResponses.TodoUpdated,
                Data = starStatus
            };
        }

        public async Task<ApiResponse<bool>> DeleteTodo(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            var todo = await _unitOfWork.TodosRepository.GetTodoByIdAsync(id);

            if (todo is null) throw new TodoNotFoundException(id);

            await _unitOfWork.TodosRepository.DeleteTodoAsync(id);

            await _unitOfWork.CommitTransactionAsync();

            return new ApiResponse<bool>
            {
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Message = SystemConstants.TodoMessageResponses.TodoDeleted
            };
        }
    }
}
