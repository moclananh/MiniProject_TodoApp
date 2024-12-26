using AutoMapper;
using TodoApp.Domain.Models.Entities;
using TodoApp.Domain.Models.Enums;
using TodoApp.Infrastructure.Dtos.TodoDtos;
using TodoApp.Infrastructure.Dtos.UserDtos;

namespace TodoApp.Infrastructure.Mappers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<LoginVm, User>().ReverseMap();


            // Entity to DTO
            CreateMap<Todo, TodoVm>().ReverseMap();

            CreateMap<Todo, CreateTodoRequest>().ReverseMap();
            // UpdateTodoRequest Mapping
            CreateMap<Todo, UpdateTodoRequest>().ReverseMap();
        }
    }
}
