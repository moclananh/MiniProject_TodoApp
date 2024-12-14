

using TodoApp.Domain.Models.Enums;
using TodoApp.Infrastructure.Pagination;

namespace TodoApp.Infrastructure.Dtos.TodoDtos
{
    public class FilterRequest : PagingRequest
    {
        public string? Title { get; set; }
        public string? Priority { get; set; }
        public TodoStatus? Status { get; set; }
        public bool? Star {  get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
