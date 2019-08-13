using DummyTodoApp.Core.Domain;
using DummyTodoApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyTodoApp.Infrastructure.Data.TodoRepository
{
    public class TodoRepository : ITodoRepository
    {
        readonly TodoContext context;

        public TodoRepository(TodoContext ctx) => context = ctx ?? throw new ArgumentException(nameof(ctx));

        public async Task Add(Todo todo)
        {
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Todo>> Get(string owner)
        {
            return await Task.Run(() => context.Todos.Where(t => t.Owner == owner).ToList());
        }

        public async Task<Todo> Get(Guid id)
        {
            return await Task.Run(() => context.Todos.SingleOrDefault(t => t.Id == id));
        }
    }
}
