using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Todo.Application.Exceptions.UserExceptions;
using TodoApp.Domain.Models.Entities;
using TodoApp.Infrastructure;
using TodoApp.Infrastructure.Dtos.UserDtos;

namespace Todo.Application.Services.UserServices
{
    public class UserService : IUserService
    {

        private readonly AppSetting _app;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IOptions<AppSetting> appOptions)
        {
            _unitOfWork = unitOfWork;
            _app = appOptions.Value;
        }

        public async Task<LoginResponse> Authencate(LoginVm request)
        {
            var authResponse = await _unitOfWork.UserRepository.AuthenticateUser(request);

            // Handle user not found case
            if (authResponse.Result == -1 || authResponse.User == null)
            {
                throw new UserNotFoundException();
            }

            // Password verification using PasswordHasher
            var passwordHasher = new PasswordHasher<LoginVm>();
            var verificationResult = passwordHasher.VerifyHashedPassword(request, authResponse.User.Password, request.Password);

            // If the password verification fails, throw exception
            if (verificationResult == PasswordVerificationResult.Failed)
            {
                throw new UserBadRequestException("Incorrect password.");
            }

            // If authentication is successful, return login response
            return new LoginResponse
            {
                Success = true,
                Message = "Login successful!",
                Id = authResponse.User.Id,
                UserName = authResponse.User.UserName,
                Email = authResponse.User.Email,
                Data = GenerateToken(authResponse.User)
            };
        }

        public async Task<ApiResponse> Register(RegisterVm request)
        {
            // Call the repository via Unit of Work
            var result = await _unitOfWork.UserRepository.RegisterUser(request);

            // Handle result
            return result switch
            {
                -1 => throw new UserBadRequestException("Email is already registered. Please use a different email."),
                -2 => throw new UserBadRequestException("Username is already registered. Please use a different username."),
                1 => new ApiResponse
                {
                    Success = true,
                    Message = "Registration Successful!"
                },
                _ => throw new InternalServerException("Unexpected error during registration.")
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
