using DummyTodoApp.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace DummyTodoApp.Infrastructure.Data.TodoRepository
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
    }
}
