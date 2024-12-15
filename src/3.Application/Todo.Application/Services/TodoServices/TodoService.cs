using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApp.Domain.Models;
using TodoApp.Domain.Models.EF;
using TodoApp.Infrastructure.Dtos.TodoDtos;

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

        public async Task<ApiResponse> GetAllTodos(FilterRequest request)
        {
            var statusParameter = request.Status.HasValue ? request.Status.Value.ToString() : (object)DBNull.Value;

            var todos = await _dbContext.Todos
            .FromSqlRaw("EXEC dbo.GetTodosWithPaging @PageNumber, @PageSize, @SearchTerm, @Priority, @Status, @Star, @IsActive, @CreatedDate, @EndDate",
                new SqlParameter("@PageNumber", request.PageNumber),
                new SqlParameter("@PageSize", request.PageSize),
                new SqlParameter("@SearchTerm", request.Title ?? (object)DBNull.Value),
                new SqlParameter("@Priority", request.Priority ?? (object)DBNull.Value),
                new SqlParameter("@Status", statusParameter),
                new SqlParameter("@Star", request.Star ?? (object)DBNull.Value),
                new SqlParameter("@IsActive", request.IsActive ?? (object)DBNull.Value),
                new SqlParameter("@CreatedDate", request.CreatedDate ?? (object)DBNull.Value),
                new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value))
            .ToListAsync();

            var todoListVm = _mapper.Map<List<TodoVm>>(todos);

            return new ApiResponse
            {
                Success = true,
                Message = "Todos fetched successfully.",
                Data = todoListVm
            };
        }

        public async Task<ApiResponse> GetTodoById(int id)
        {
            var todo = (await _dbContext.Todos
              .FromSqlInterpolated($"EXEC dbo.GetTodoById @Id = {id}")
              .ToListAsync())
              .SingleOrDefault();

            if (todo == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Todo not found!"
                };
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
                    new SqlParameter("@Desciption", (object?)todoVm.Description ?? DBNull.Value),
                    new SqlParameter("@Status", todoVm.Status.ToString()),
                    new SqlParameter("@Priority", todoVm.Priority),
                    new SqlParameter("@CreatedDate", todoVm.CreatedDate),
                    new SqlParameter("@EndDate", todoVm.EndDate),
                    new SqlParameter("@Star", todoVm.Star),
                    new SqlParameter("@IsActive", todoVm.IsActive)
                };

                await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.CreateTodo @Title, @Desciption, @Status, @Priority, @CreatedDate, @EndDate, @Star, @IsActive", parameters);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Todo created successfully."
                };
            }
            catch (Exception ex)
            {
                // Log exception
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Failed to create Todo. Error: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse> UpdateTodo(int id, UpdateTodoRequest todoVm)
        {
            try
            {
                // Verify if the todo exists
                var todoExists = await _dbContext.Todos.AnyAsync(t => t.Id == id);
                if (!todoExists)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Todo not found!"
                    };
                }

                var parameters = new[]
                {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Title", todoVm.Title),
                    new SqlParameter("@Desciption", (object?)todoVm.Description ?? DBNull.Value),
                    new SqlParameter("@Status", todoVm.Status.ToString()),
                    new SqlParameter("@Priority", todoVm.Priority),
                    new SqlParameter("@CreatedDate", todoVm.CreatedDate),
                    new SqlParameter("@EndDate", todoVm.EndDate),
                    new SqlParameter("@Star", todoVm.Star),
                    new SqlParameter("@IsActive", todoVm.IsActive)
                };

                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.UpdateTodo @Id, @Title, @Desciption, @Status, @Priority, @CreatedDate, @EndDate, @Star, @IsActive", parameters);

                return new ApiResponse
                {
                    Success = true,
                    Message = "Todo updated successfully."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Failed to update Todo. Error: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponse> DeleteTodo(int id)
        {
            try
            {
                // Verify if the todo exists
                var todoExists = await _dbContext.Todos.AnyAsync(t => t.Id == id);
                if (!todoExists)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "Todo not found!"
                    };
                }

                // Call the stored procedure for deletion
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
                return new ApiResponse
                {
                    Success = false,
                    Message = $"Failed to delete Todo. Error: {ex.Message}"
                };
            }
        }
    }
}
