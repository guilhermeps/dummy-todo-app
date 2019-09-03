namespace DummyTodoApp.Domain
{
    public sealed class Todo
    {
        public string Description { get; private set; }

        public string Owner { get; private set; }

        public bool Done { get; set; }

        public int ExecutionPriority { get; set; }

        public Todo(string description, string owner, int priority)
        {
            Description = description;
            Owner = owner;
            ExecutionPriority = priority;
        }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Owner) && ExecutionPriority > 0;
    }
}
