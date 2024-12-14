

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApp.Domain.Models;
using TodoApp.Domain.Models.EF;
using TodoApp.Domain.Models.Entities;
using TodoApp.Infrastructure.Dtos.UserDtos;

namespace Todo.Application.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly AppSetting _app;

        public UserService(ApplicationDbContext context, IConfiguration configuration, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _dbContext = context;
            _config = configuration;
            _app = optionsMonitor.CurrentValue;
        }

        public async Task<ApiResponse> Authencate(LoginVm request)
        {
            // Retrieve the user by email asynchronously
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "User does not exist!"
                };
            }

            // Verify the password
            var passwordHasher = new PasswordHasher<User>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, request.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Password is incorrect!"
                };
            }

            // Generate a token if the password is valid
            return new ApiResponse
            {
                Success = true,
                Message = "Login Success!",
                Data = GenerateToken(user)
            };
        }

        public async Task<ApiResponse> Register(RegisterVm request)
        {
            // Check if a user with the same email already exists in the database
            var existingUser = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser != null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Email is already registered. Please use a different email."
                };
            }

            // Create a new user based on the registration request
            var passwordHasher = new PasswordHasher<User>();
            var newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = passwordHasher.HashPassword(null, request.Password) // Hash the password
            };

            // Add the new user to the database asynchronously
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return new ApiResponse
            {
                Success = true,
                Message = "Registration Successful!"
            };
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
                Expires = DateTime.UtcNow.AddMinutes(1), // Set token expiration
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescription);
            return jwtTokenHandler.WriteToken(token);
        }
    }
}
