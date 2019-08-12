using System;

namespace DummyTodoApp.Core.Domain
{
    public class Todo
    {
        public Guid Id { get; private set; }

        public string Description { get; private set; }

        public string Owner { get; private set; }

        public bool Read { get; set; }

        public Todo()
        {
            Id = Guid.NewGuid();
        }

        public Todo(string description, string owner)
        {
            Id = Guid.NewGuid();
            Description = description;
            Owner = owner;
        }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Owner) && !(Id == Guid.Empty);
    }
}
