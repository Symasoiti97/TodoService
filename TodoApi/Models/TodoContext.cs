﻿using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItemDb>(builder =>
            {
                builder.ToTable("TodoItems");
                builder.HasKey(x => x.Id);
            });
        }
    }
}