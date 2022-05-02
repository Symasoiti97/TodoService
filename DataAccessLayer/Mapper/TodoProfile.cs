using AutoMapper;
using BusinessLayer.Dto;
using BusinessLayer.Entities;

namespace DataAccessLayer.Mapper
{
    internal class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<TodoItem, TodoItemDto>();
        }
    }
}