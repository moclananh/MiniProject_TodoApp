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

        public async Task<ApiResponse> Authencate(LoginVm request)
        {
            var parameters = new[]
            {
                new SqlParameter("@Email", request.Email),
                new SqlParameter("@Password", request.Password),
                new SqlParameter("@UserId", SqlDbType.UniqueIdentifier) { Direction = ParameterDirection.Output },
                new SqlParameter("@UserName", SqlDbType.NVarChar, 256) { Direction = ParameterDirection.Output },
                new SqlParameter("@EmailOut", SqlDbType.NVarChar, 256) { Direction = ParameterDirection.Output },
                new SqlParameter("@HashedPassword", SqlDbType.NVarChar, -1) { Direction = ParameterDirection.Output }, // Output hashed password
                new SqlParameter("@Result", SqlDbType.Int) { Direction = ParameterDirection.Output }
            };

            await _dbContext.Database.ExecuteSqlRawAsync(
                "EXEC dbo.AuthenticateUser @Email, @Password, @UserId OUTPUT, @UserName OUTPUT, @EmailOut OUTPUT, @HashedPassword OUTPUT, @Result OUTPUT",
                parameters);

            // Response code
            var result = (int)parameters[6].Value;

            // User not exist
            if (result == -1)
            {
                throw new UserNotFoundException();
            }

            if (result == 1)
            {
                var hashedPassword = (string)parameters[5].Value;
                var passwordHasher = new PasswordHasher<LoginVm>();
                var verificationResult = passwordHasher.VerifyHashedPassword(request, hashedPassword, request.Password);

                if (verificationResult == PasswordVerificationResult.Failed)
                {
                    throw new UserBadRequestException("Wrong password.");
                }

                var user = new User
                {
                    Id = (Guid)parameters[2].Value,
                    UserName = (string)parameters[3].Value,
                    Email = (string)parameters[4].Value,
                };

                return new ApiResponse
                {
                    Success = true,
                    Message = "Login successful!",
                    Data = GenerateToken(user)
                };
            }

            throw new InternalServerException("Error from server.");
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

            await _dbContext.Database.ExecuteSqlRawAsync("EXEC dbo.RegisterUser @UserName, @Email, @Password, @Result OUTPUT", parameters);

            var result = (int)parameters[3].Value;

            // Handle result codes
            if (result == -1)
            {
                throw new UserBadRequestException("Email is already registered. Please use a different email.");
            }

            if (result == -2)
            {
                throw new UserBadRequestException("Username is already registered. Please use a different username.");
            }

            if (result == 1)
            {
                return new ApiResponse
                {
                    Success = true,
                    Message = "Registration Successful!"
                };
            }
            throw new InternalServerException("Error from server");
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
