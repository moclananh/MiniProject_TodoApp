
using TodoApp.Domain.Models.Enums;

namespace TodoApp.Domain.Models.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; } = TodoStatus.Todo;
        public int Priority { get; set; } 
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public bool Star { get; set; } 
        public bool IsActive { get; set; } 
        public Guid UserId { get; set; }
        public User User { get; set; } 
    }
}
