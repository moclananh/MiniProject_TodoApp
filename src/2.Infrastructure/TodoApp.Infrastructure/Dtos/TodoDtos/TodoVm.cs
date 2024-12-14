﻿using TodoApp.Domain.Models.Enums;

namespace TodoApp.Infrastructure.Dtos.TodoDtos
{
    public class TodoVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public string Status { get; set; } 
        public int Priority { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Star { get; set; } 
        public bool IsActive { get; set; }
    }
}