using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.UserServices;
using TodoApp.Domain.Models;
using TodoApp.Infrastructure.Dtos.UserDtos;

namespace Todo.TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login([FromBody] LoginVm request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data provided."
                });
            }

            var response = await _userService.Authencate(request);
            return Ok(response);
        }

        [HttpPost("Register")]
        [Authorize]
        public async Task<IActionResult> Register([FromBody] RegisterVm request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data provided."
                });
            }

            var response = await _userService.Register(request);
            return Ok(response);
        }
    }
}
