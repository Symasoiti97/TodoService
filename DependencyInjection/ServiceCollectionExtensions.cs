using System;
using BusinessLayer;
using DataAccessLayer;
using DataAccessLayer.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTodoServices(this IServiceCollection services, Action<DbContextOptionsBuilder> actionDbContextOptionsBuilder)
        {
            services.AddDbContext<TodoContext>(actionDbContextOptionsBuilder);
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<ITodoService, TodoService>();
            services.AddAutoMapper(x => x.AddProfile<TodoProfile>());

            return services;
        }
    }
}