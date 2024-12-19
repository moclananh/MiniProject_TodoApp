﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.TodoServices;
using TodoApp.Infrastructure.Dtos.TodoDtos;

namespace Todo.TodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodos([FromQuery] FilterRequest request)
        {
            var response = await _todoService.GetAllTodos(request);
            return Ok(response);
        }

        [HttpGet("GetTodosByUserId/{userId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTodosByUserId(Guid userId, [FromQuery] FilterRequest request)
        {
            var response = await _todoService.GetTodosByUserId(userId, request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var response = await _todoService.GetTodoById(id);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest todoVm)
        {
            var response = await _todoService.CreateTodo(todoVm);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoRequest todoVm)
        {
            var response = await _todoService.UpdateTodo(id, todoVm);
            return Ok(response);
        }

        [HttpPatch("StarUpdate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> StarUpdate(int id)
        {
            var response = await _todoService.StarUpdate(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var response = await _todoService.DeleteTodo(id);
            return Ok(response);
        }
    }

}
