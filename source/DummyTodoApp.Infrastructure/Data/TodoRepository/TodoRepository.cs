using DummyTodoApp.Core.Repositories;
using DummyTodoApp.Domain;
using DummyTodoApp.Infrastructure.Data.TodoRepository.Mappers;
using DummyTodoApp.Infrastructure.Data.TodoRepository.Models;
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

        public async Task<IList<Todo>> GetAllUnreadTodos()
        {
            var todoList = context.Todos.Where(t => t.Done == false).ToList();
            IList<Todo> todoDomainList = new List<Todo>();
            todoList.ForEach(model => todoDomainList.Add(TodoMapper.ToDomain(model)));
            return await Task.FromResult(todoDomainList);
        }

        public async Task Update(Todo todo)
        {
            var existedTodo = GetUnreadTodosFromGuilherme();
            if (existedTodo != null) 
            {
                existedTodo.Done = todo.Done;
                context.Entry(existedTodo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Todos.Update(existedTodo);
                await context.SaveChangesAsync();
            }
        }

        private TodoModel GetUnreadTodosFromGuilherme()
        {
            var todo = context.Todos.Where(t => t.Done == false && t.Owner.ToLowerInvariant() == "guilherme").FirstOrDefault();
            return todo;
        }
    }
}
