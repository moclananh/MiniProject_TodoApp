
using TodoApp.Domain.Models.Entities;

namespace TodoApp.Infrastructure.Dtos.TodoDtos
{
    public class TodoResult
    {
        public List<Todo> Todos { get; set; }
        public int TotalCount { get; set; }
    }
}
