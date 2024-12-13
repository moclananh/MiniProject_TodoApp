
using TodoApp.Domain.Models.Enums;

namespace TodoApp.Domain.Models.Entities
{
    public class Todo
    {
        public int Id { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public TodoStatus Status { get; set; } = TodoStatus.Todo;
        public int Priority { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Star { get; set; } = default!;
        public bool IsActive { get; set; } = default!;

    }
}
