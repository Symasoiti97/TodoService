using DataAccessLayer.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    internal class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
        }
    }
}