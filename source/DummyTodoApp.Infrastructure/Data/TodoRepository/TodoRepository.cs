using DummyTodoApp.Core.Domain;
using DummyTodoApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DummyTodoApp.Infrastructure.Data.TodoRepository
{
    public class TodoRepository : ITodoRepository
    {
        readonly TodoContext context;

        public TodoRepository(TodoContext ctx)
        {
            context = ctx;
        }

        public void Add(Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
        }

        public IList<Todo> Get(string owner) => context.Todos.Where(t => t.Owner == owner).ToList();

        public Todo Get(Guid id) => context.Todos.SingleOrDefault(t => t.Id == id);

        public IList<Todo> GetAllUnreadTodos() => context.Todos.Where(t => t.Read == false).ToList();

        public void Update(Todo todo) 
        {
            var existingTodo = Get(todo.Id);
            if (existingTodo != null) 
            {
                existingTodo.Read = todo.Read;
                context.Entry(existingTodo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.Update(context.Todos);
                context.SaveChanges();
            }
        }

        public void Remove(Guid id)
        {
            var todo = Get(id);
            context.Todos.Remove(todo);
            context.SaveChanges();
        }
    }
}
