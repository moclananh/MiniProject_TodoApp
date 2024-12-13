using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.TodoServices;
using TodoApp.Infrastructure.Dtos.TodoDtos;
using TodoApp.Infrastructure.Pagination;

namespace Todo.TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos([FromQuery] PagingRequest pagingRequest, string? keyword)
        {
            var response = await _todoService.GetAllTodos(pagingRequest, keyword);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var response = await _todoService.GetTodoById(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest todoVm)
        {
            var response = await _todoService.CreateTodo(todoVm);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoRequest todoVm)
        {
            var response = await _todoService.UpdateTodo(id, todoVm);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var response = await _todoService.DeleteTodo(id);
            return Ok(response);
        }
    }

}
