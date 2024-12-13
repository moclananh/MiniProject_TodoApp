
using TodoApp.Domain.Models.Enums;

namespace TodoApp.Infrastructure.Dtos.TodoDtos
{
    public class CreateTodoRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Star { get; set; }
        public bool IsActive { get; set; }
    }
}
