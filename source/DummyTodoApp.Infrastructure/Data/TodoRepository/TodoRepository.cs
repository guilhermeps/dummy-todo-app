using DummyTodoApp.Core.Repositories;
using DummyTodoApp.Domain;
using DummyTodoApp.Infrastructure.Data.TodoRepository.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyTodoApp.Infrastructure.Data.TodoRepository
{
    public sealed class TodoRepository : ITodoRepository
    {
        readonly TodoContext context;

        public TodoRepository(TodoContext ctx) => context = ctx ?? throw new ArgumentException(nameof(ctx));

        public async Task Add(Todo todo)
        {
            var todoModel = TodoMapper.ToModel(todo);
            await context.Todos.AddAsync(todoModel);
            await context.SaveChangesAsync();
        }

        public async Task<IList<Todo>> Get(string owner)
        {
            var todoList = context.Todos.Where(t => t.Owner == owner).ToList();
            IList<Todo> todoDomainList = new List<Todo>();
            todoList.ForEach(model => todoDomainList.Add(TodoMapper.ToDomain(model)));
            return await Task.FromResult(todoDomainList);
        }

        public async Task<Todo> Get(Guid id)
        {
            var todo = context.Todos.SingleOrDefault(t => t.Id == id);
            var todoDomain = TodoMapper.ToDomain(todo);
            return await Task.FromResult(todoDomain);
        }
    }
}
