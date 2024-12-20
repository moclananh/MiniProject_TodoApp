
namespace Todo.Application.Services.TodoServices
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public TodoService(ApplicationDbContext dbContext, IMapper mapper, IConfiguration config)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _config = config;
        }

        public async Task<ApiResponse<PagingResult<TodoVm>>> GetAllTodos(FilterRequest request)
        {
            try
            {
                var statusParameter = request.Status.HasValue ? request.Status.Value.ToString() : (object)DBNull.Value;

                var todos = await _dbContext.Todos
                    .FromSqlRaw("EXEC dbo.GetTodosWithPaging @PageNumber, @PageSize, @SearchTerm, @Priority, @Status, @Star, @IsActive, @StartDate, @EndDate, @CreatedDate",
                        new SqlParameter("@PageNumber", request.PageNumber),
                        new SqlParameter("@PageSize", request.PageSize),
                        new SqlParameter("@SearchTerm", request.Title ?? (object)DBNull.Value),
                        new SqlParameter("@Priority", request.Priority ?? (object)DBNull.Value),
                        new SqlParameter("@Status", statusParameter),
                        new SqlParameter("@Star", request.Star ?? (object)DBNull.Value),
                        new SqlParameter("@IsActive", request.IsActive ?? (object)DBNull.Value),
                        new SqlParameter("@StartDate", request.CreatedDate ?? (object)DBNull.Value),
                        new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value),
                        new SqlParameter("@CreatedDate", request.EndDate ?? (object)DBNull.Value))
                    .ToListAsync();

                var totalCount = await _dbContext.Todos.CountAsync();

                var todoListVm = _mapper.Map<List<TodoVm>>(todos);

                var pagingResult = new PagingResult<TodoVm>(request.PageNumber, request.PageSize, totalCount, todoListVm);

                return new ApiResponse<PagingResult<TodoVm>>
                {
                    Success = true,
                    Message = "Todos fetched successfully.",
                    Data = pagingResult
                };
            }
            catch (Exception ex)
            {
                throw new TodoBadRequestException("Fetch data failed. Error: ", ex.Message);
            }
        }

        public async Task<ApiResponse<PagingResult<TodoVm>>> GetTodosByUserId(Guid userId, FilterRequest request)
        {

            try
            {
                var statusParameter = request.Status.HasValue ? request.Status.Value.ToString() : (object)DBNull.Value;

                // Define output parameter for TotalItem
                var totalItemParam = new SqlParameter
                {
                    ParameterName = "@TotalItem",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                // Execute the stored procedure
                var todos = await _dbContext.Todos
                    .FromSqlRaw(
                        "EXEC dbo.GetTodosByUserIdWithPaging @UserId, @PageNumber, @PageSize, @SearchTerm, @Priority, @Status, @Star, @IsActive, @StartDate, @EndDate, @CreatedDate, @TotalItem OUTPUT",
                        new SqlParameter("@UserId", userId),
                        new SqlParameter("@PageNumber", request.PageNumber),
                        new SqlParameter("@PageSize", request.PageSize),
                        new SqlParameter("@SearchTerm", request.Title ?? (object)DBNull.Value),
                        new SqlParameter("@Priority", request.Priority ?? (object)DBNull.Value),
                        new SqlParameter("@Status", statusParameter),
                        new SqlParameter("@Star", request.Star ?? (object)DBNull.Value),
                        new SqlParameter("@IsActive", request.IsActive ?? (object)DBNull.Value),
                        new SqlParameter("@StartDate", request.StartDate ?? (object)DBNull.Value),
                        new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value),
                        new SqlParameter("@CreatedDate", request.CreatedDate ?? (object)DBNull.Value),
                        totalItemParam)
                    .ToListAsync();

                // Get the total count from the output parameter
                int totalCount = (int)(totalItemParam.Value ?? 0);

                var todoListVm = _mapper.Map<List<TodoVm>>(todos);

                var pagingResult = new PagingResult<TodoVm>(request.PageNumber, request.PageSize, totalCount, todoListVm);

                return new ApiResponse<PagingResult<TodoVm>>
                {
                    Success = true,
                    Message = "Todos fetched successfully.",
                    Data = pagingResult
                };
            }
            catch (Exception ex)
            {
                throw new TodoBadRequestException("Fetch data failed. Error: ", ex.Message);
            }
        }

        public async Task<ApiResponse> GetTodoById(int id)
        {
            var todo = (await _dbContext.Todos
              .FromSqlInterpolated($"EXEC dbo.GetTodoById @Id = {id}")
              .ToListAsync())
              .SingleOrDefault();

            if (todo is null)
            {
                throw new TodoNotFoundException(id);
            }

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
            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Title", todoVm.Title),
                    new SqlParameter("@Description", (object?)todoVm.Description ?? DBNull.Value),
                    new SqlParameter("@Status", todoVm.Status.ToString()),
                    new SqlParameter("@Priority", todoVm.Priority),
                    new SqlParameter("@StarDate", (object?)todoVm.StartDate ?? DBNull.Value),
                    new SqlParameter("@EndDate", (object?)todoVm.EndDate ?? DBNull.Value),
                    new SqlParameter("@CreatedDate", todoVm.CreatedDate ?? DateTime.UtcNow),
                    new SqlParameter("@Star", todoVm.Star),
                    new SqlParameter("@IsActive", todoVm.IsActive),
                    new SqlParameter("@UserId", todoVm.UserId)
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.CreateTodo @Title, @Description, @Status, @Priority, @StarDate, @EndDate, @CreatedDate, @Star, @IsActive, @UserId",
                    parameters);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Todo created successfully."
                };
            }
            catch (Exception ex)
            {
                throw new TodoBadRequestException("Create failed. Error: ", ex.Message);
            }
        }

        public async Task<ApiResponse> UpdateTodo(int id, UpdateTodoRequest todoVm)
        {
            await GetTodoById(id);

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Title", todoVm.Title),
                    new SqlParameter("@Description", (object?)todoVm.Description ?? DBNull.Value),
                    new SqlParameter("@Status", todoVm.Status.ToString()),
                    new SqlParameter("@Priority", todoVm.Priority),
                    new SqlParameter("@StarDate", (object?)todoVm.StartDate ?? DBNull.Value),
                    new SqlParameter("@EndDate", (object?)todoVm.EndDate ?? DBNull.Value),
                    new SqlParameter("@Star", todoVm.Star),
                    new SqlParameter("@IsActive", todoVm.IsActive)
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.UpdateTodo @Id, @Title, @Description, @Status, @Priority, @StarDate, @EndDate, @Star, @IsActive",
                    parameters);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Todo updated successfully."
                };
            }
            catch (Exception ex)
            {
                throw new TodoBadRequestException("Update failed. Error: ", ex.Message);
            }
        }

        public async Task<ApiResponse> DeleteTodo(int id)
        {
            await GetTodoById(id);
            try
            {
                var parameter = new SqlParameter("@Id", id);
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteTodo @Id", parameter);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Todo deleted successfully."
                };
            }
            catch (Exception ex)
            {
                throw new TodoBadRequestException("Delete failed. Error: ", ex.Message);
            }
        }

        public async Task<ApiResponse> StarUpdate(int id)
        {
            await GetTodoById(id);
            try
            {
                var starParameter = new SqlParameter
                {
                    ParameterName = "@Result",
                    SqlDbType = System.Data.SqlDbType.Bit,
                    Direction = System.Data.ParameterDirection.Output
                };

                var parameters = new[]
                {
                    new SqlParameter("@Id", id),
                    starParameter
                };

                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.ToggleTodoStar @Id, @Result OUTPUT",
                    parameters);

                var newStarValue = (bool)starParameter.Value;

                return new ApiResponse
                {
                    Success = true,
                    Message = $"Todo star toggled successfully. New value: {newStarValue}",
                    Data = newStarValue
                };
            }
            catch (Exception ex)
            {
                throw new TodoBadRequestException("Failed to toggle star. Error: ", ex.Message);
            }
        }
    }
}
