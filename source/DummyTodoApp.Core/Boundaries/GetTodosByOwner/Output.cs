using DummyTodoApp.Domain;
using System.Collections.Generic;

namespace DummyTodoApp.Core.Boundaries.GetTodosByOwner
{
    public sealed class OutputItem
    {
        public string Description { get; set; }

        public string Owner { get; set; }

        public bool Done { get; set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Owner);
    }

    public sealed class Output
    {
        public IList<OutputItem> TodoList { get; set; }

        public Output(IList<Todo> todoList)
        {
            TodoList = new List<OutputItem>();
            foreach (var todo in todoList)
                TodoList.Add(new OutputItem
                {
                    Description = todo.Description,
                    Owner = todo.Owner,
                    Done = todo.Done
                });
        }
    }
}
