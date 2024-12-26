using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TodoApp.BuildingBlock.Exceptions;
using TodoApp.BuildingBlock.Utilities;
using TodoApp.Domain.Models.EF;
using TodoApp.Domain.Models.Entities;
using TodoApp.Infrastructure.Dtos.TodoDtos;

namespace TodoApp.Infrastructure.Repositories.TodosRepositories
{
    public class TodosRepository : ITodosRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TodosRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TodoResult> GetAllTodosAsync(FilterRequest request)
        {
            var totalItemParam = new SqlParameter
            {
                ParameterName = "@TotalItem", // Output parameter for total item count
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };
            try
            {
                // Execute the stored procedure and get the todos
                var todos = await _dbContext.Todos
                    .FromSqlRaw(
                        "EXEC dbo.GetTodosWithPaging @PageNumber, @PageSize, @SearchTerm, @Priority, @Status, @Star, @IsActive, @StartDate, @EndDate, @CreatedDate, @TotalItem OUTPUT",
                        new SqlParameter("@PageNumber", request.PageNumber),
                        new SqlParameter("@PageSize", request.PageSize),
                        new SqlParameter("@SearchTerm", request.Title ?? (object)DBNull.Value),
                        new SqlParameter("@Priority", request.Priority ?? (object)DBNull.Value),
                        new SqlParameter("@Status", request.Status ?? (object)DBNull.Value),
                        new SqlParameter("@Star", request.Star ?? (object)DBNull.Value),
                        new SqlParameter("@IsActive", request.IsActive ?? (object)DBNull.Value),
                        new SqlParameter("@StartDate", request.StartDate ?? (object)DBNull.Value),
                        new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value),
                        new SqlParameter("@CreatedDate", request.CreatedDate ?? (object)DBNull.Value),
                        totalItemParam)
                    .ToListAsync();

                // Check if totalItemParam.Value is null
                int totalCount = totalItemParam.Value != DBNull.Value ? (int)totalItemParam.Value : 0;

                // Return both the todos and the total count encapsulated in TodoResult
                return new TodoResult
                {
                    Todos = todos,
                    TotalCount = totalCount
                };
            }
             catch (Exception ex)
            {
                throw new BadRequestException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        public async Task<TodoResult> GetTodosByUserIdAsync(Guid userId, FilterRequest request)
        {
            var totalItemParam = new SqlParameter
            {
                ParameterName = "@TotalItem", // Output parameter for total item count
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };
            try
            {
                // Execute the stored procedure and get the todos
                var todos = await _dbContext.Todos
                    .FromSqlRaw(
                        "EXEC dbo.GetTodosByUserIdWithPaging @UserId, @PageNumber, @PageSize, @SearchTerm, @Priority, @Status, @Star, @IsActive, @StartDate, @EndDate, @CreatedDate, @TotalItem OUTPUT",
                        new SqlParameter("@UserId", userId),
                        new SqlParameter("@PageNumber", request.PageNumber),
                        new SqlParameter("@PageSize", request.PageSize),
                        new SqlParameter("@SearchTerm", request.Title ?? (object)DBNull.Value),
                        new SqlParameter("@Priority", request.Priority ?? (object)DBNull.Value),
                        new SqlParameter("@Status", request.Status ?? (object)DBNull.Value),
                        new SqlParameter("@Star", request.Star ?? (object)DBNull.Value),
                        new SqlParameter("@IsActive", request.IsActive ?? (object)DBNull.Value),
                        new SqlParameter("@StartDate", request.StartDate ?? (object)DBNull.Value),
                        new SqlParameter("@EndDate", request.EndDate ?? (object)DBNull.Value),
                        new SqlParameter("@CreatedDate", request.CreatedDate ?? (object)DBNull.Value),
                        totalItemParam)
                    .ToListAsync();

                // Check if totalItemParam.Value is null
                int totalCount = totalItemParam.Value != DBNull.Value ? (int)totalItemParam.Value : 0;

                // Return both the todos and the total count encapsulated in TodoResult
                return new TodoResult
                {
                    Todos = todos,
                    TotalCount = totalCount
                };
            }
            catch (Exception ex)
            {
                throw new BadRequestException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }

        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            try
            {
                var result = (await _dbContext.Todos
                    .FromSqlRaw($"EXEC dbo.GetTodoById @Id = {id}")
                    .ToListAsync())
                    .SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error when calling store procedure. Error: ", ex.Message);
            }
        }

        public async Task CreateTodoAsync(CreateTodoRequest todoVm)
        {
            var parameters = new[]
               {
                    new SqlParameter("@Title", todoVm.Title),
                    new SqlParameter("@Description", (object?)todoVm.Description ?? DBNull.Value),
                    new SqlParameter("@Status", todoVm.Status),
                    new SqlParameter("@Priority", todoVm.Priority),
                    new SqlParameter("@StarDate", (object?)todoVm.StartDate ?? DBNull.Value),
                    new SqlParameter("@EndDate", (object?)todoVm.EndDate ?? DBNull.Value),
                    new SqlParameter("@CreatedDate", todoVm.CreatedDate ?? DateTime.UtcNow),
                    new SqlParameter("@Star", todoVm.Star),
                    new SqlParameter("@IsActive", todoVm.IsActive),
                    new SqlParameter("@UserId", todoVm.UserId)
                };
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.CreateTodo @Title, @Description, @Status, @Priority, @StarDate, @EndDate, @CreatedDate, @Star, @IsActive, @UserId",
                    parameters);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        public async Task UpdateTodoAsync(int id, UpdateTodoRequest todoVm)
        {
            var parameters = new[]
               {
                    new SqlParameter("@Id", id),
                    new SqlParameter("@Title", todoVm.Title),
                    new SqlParameter("@Description", (object?)todoVm.Description ?? DBNull.Value),
                    new SqlParameter("@Status", todoVm.Status),
                    new SqlParameter("@Priority", todoVm.Priority),
                    new SqlParameter("@StarDate", (object?)todoVm.StartDate ?? DBNull.Value),
                    new SqlParameter("@EndDate", (object?)todoVm.EndDate ?? DBNull.Value),
                    new SqlParameter("@Star", todoVm.Star),
                    new SqlParameter("@IsActive", todoVm.IsActive)
                };
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
                  "EXEC dbo.UpdateTodo @Id, @Title, @Description, @Status, @Priority, @StarDate, @EndDate, @Star, @IsActive; " +
                  "SELECT 1;",
                  parameters);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }

        public async Task DeleteTodoAsync(int id)
        {
            var parameter = new SqlParameter("@Id", id);
            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.DeleteTodo @Id", parameter);
            }
            catch (Exception ex)
            {
                throw new BadRequestException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }

        }

        public async Task<bool> ToggleTodoStarAsync(int id)
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

            try
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.ToggleTodoStar @Id, @Result OUTPUT",
                    parameters);

                return (bool)starParameter.Value;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(SystemConstants.InternalMessageResponses.DatabaseBadResponse, ex.Message);
            }
        }
    }
}
