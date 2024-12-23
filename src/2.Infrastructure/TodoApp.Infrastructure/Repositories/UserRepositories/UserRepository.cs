using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TodoApp.BuildingBlock.Exceptions;
using TodoApp.Domain.Models.EF;
using TodoApp.Domain.Models.Entities;
using TodoApp.Infrastructure.Dtos.UserDtos;


namespace TodoApp.Infrastructure.Repositories.UserRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuthenticateResponse> AuthenticateUser(LoginVm request)
        {
            var parameters = new[]
              {
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Password", request.Password),
                new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output },
                new SqlParameter("@UserName", SqlDbType.NVarChar, 256) { Direction = ParameterDirection.Output },
                new SqlParameter("@EmailOut", SqlDbType.NVarChar, 256) { Direction = ParameterDirection.Output },
                new SqlParameter("@HashedPassword", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output },
                new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            try
            {
                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.AuthenticateUser @Email, @Password, @UserId OUTPUT, @UserName OUTPUT, @EmailOut OUTPUT, @HashedPassword OUTPUT, @Result OUTPUT",
                    parameters);

                // Extract the result from output parameters
                var result = (int)parameters.First(p => p.ParameterName == "@Result").Value!;

                // If authentication failed (result code -1), return null user
                if (result == -1)
                {
                    return new AuthenticateResponse
                    {
                        Result = result,
                        User = null
                    };
                }

                // Map the user object from the output parameters
                var user = new User
                {
                    Id = (Guid)parameters.First(p => p.ParameterName == "@UserId").Value!,
                    UserName = (string)parameters.First(p => p.ParameterName == "@UserName").Value!,
                    Email = (string)parameters.First(p => p.ParameterName == "@EmailOut").Value!,
                    Password = (string)parameters.First(p => p.ParameterName == "@HashedPassword").Value!
                };

                // Return the authentication response
                return new AuthenticateResponse
                {
                    Result = result,
                    User = user
                };
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error in call store procedure, Error: ", ex.Message);
            }
        }

        public async Task<int> RegisterUser(RegisterVm request)
        {
            // Hash the password before storing it
            var passwordHasher = new PasswordHasher<RegisterVm>();
            var hashedPassword = passwordHasher.HashPassword(request, request.Password);

            // Prepare the parameters for the stored procedure
            var parameters = new[]
            {
                new SqlParameter("@UserName", request.UserName),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Password", hashedPassword),
                new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };
            try
            {
                // Execute the stored procedure
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.RegisterUser @UserName, @Email, @Password, @Result OUTPUT",
                    parameters);

                // Return the result from output parameters
                return (int)parameters.First(p => p.ParameterName == "@Result").Value!;
            }
            catch (Exception ex)
            {
                throw new BadRequestException("Error in call store procedure, Error: ", ex.Message);
            }
        }
    }
}

