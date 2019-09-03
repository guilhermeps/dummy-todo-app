namespace DummyTodoApp.Core.UseCases.AddTodo.Boundaries
{
    public sealed class Input
    {
        public string Description { get; set; }

        public string Owner { get; set; }

        public int ExecutionPriority { get; set; }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Owner) && ExecutionPriority > 0;
    }
}
