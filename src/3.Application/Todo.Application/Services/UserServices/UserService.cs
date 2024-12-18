using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo.Application.Exceptions.UserExceptions;
using TodoApp.Domain.Models.Entities;
using TodoApp.Infrastructure.Dtos.UserDtos;

namespace Todo.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AppSetting _app;

        public UserService(ApplicationDbContext context, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _dbContext = context;
            _app = optionsMonitor.CurrentValue;
        }

        public async Task<LoginResponse> Authencate(LoginVm request)
        {
            // Define SQL parameters
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

            // Execute the stored procedure
            await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.AuthenticateUser @Email, @Password, @UserId OUTPUT, @UserName OUTPUT, @EmailOut OUTPUT, @HashedPassword OUTPUT, @Result OUTPUT",
                parameters);

            // Extract the result code
            var result = (int)parameters[6].Value;

            // Handle different result codes
            if (result == -1)
            {
                throw new UserNotFoundException();
            }

            if (result == 1)
            {
                // Verify the password
                var hashedPassword = (string)parameters[5].Value;
                var passwordHasher = new PasswordHasher<LoginVm>();
                var verificationResult = passwordHasher.VerifyHashedPassword(request, hashedPassword, request.Password);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    throw new UserBadRequestException("Incorrect password.");
                }

                var loginResponse = new LoginResponse
                {
                    Success = true,
                    Message = "Login successful!",
                    Id = (Guid)parameters[2].Value,
                    UserName = (string)parameters[3].Value,
                    Email = (string)parameters[4].Value
                };

                // Optionally, you can also include the token or other login-related data here if needed
                loginResponse.Data = GenerateToken(new User
                {
                    Id = loginResponse.Id,
                    UserName = loginResponse.UserName,
                    Email = loginResponse.Email
                });

                return loginResponse;
            }

            // Handle unexpected results
            throw new InternalServerException("Unexpected server error during authentication.");


        }

        public async Task<ApiResponse> Register(RegisterVm request)
        {
            var passwordHasher = new PasswordHasher<RegisterVm>();
            var hashedPassword = passwordHasher.HashPassword(request, request.Password); // Hash the password

            var parameters = new[]
            {
                new SqlParameter("@UserName", request.UserName),
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Password", hashedPassword),
                new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            // Execute stored procedure to register the user
            await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.RegisterUser @UserName, @Email, @Password, @Result OUTPUT",
                parameters
            );

            // Get the result code
            var result = (int)parameters[3].Value;

            // Handle result codes
            switch (result)
            {
                case -1:
                    throw new UserBadRequestException("Email is already registered. Please use a different email.");
                case -2:
                    throw new UserBadRequestException("Username is already registered. Please use a different username.");
                case 1:
                    return new ApiResponse
                    {
                        Success = true,
                        Message = "Registration Successful!"
                    };
                default:
                    throw new InternalServerException("Unexpected error during registration.");
            }
        }

        private string GenerateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_app.SecretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim("TokenId", Guid.NewGuid().ToString())
            }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
