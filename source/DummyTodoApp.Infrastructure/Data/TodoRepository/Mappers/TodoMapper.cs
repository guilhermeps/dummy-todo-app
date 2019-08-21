using DummyTodoApp.Domain;
using DummyTodoApp.Infrastructure.Data.TodoRepository.Models;

namespace DummyTodoApp.Infrastructure.Data.TodoRepository.Mappers
{
    public static class TodoMapper
    {
        public static TodoModel ToModel(Todo todo)
        {
            if (todo != null)
                return new TodoModel(todo.Description, todo.Owner);
            else return null;
        }

        public static Todo ToDomain(TodoModel model)
        {
            if (model != null)
                return new Todo(model.Description, model.Owner);
            else return null;
        }
    }
}
