using DummyTodoApp.Infrastructure.Data.TodoRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace DummyTodoApp.Infrastructure.Data.TodoRepository
{
    public sealed class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<TodoModel> Todos { get; set; }
    }
}
