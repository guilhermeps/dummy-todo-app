namespace DummyTodoApp.Domain
{
    public sealed class Todo
    {
        public string Description { get; private set; }

        public string Owner { get; private set; }

        public bool Done { get; set; }

        public Todo(string description, string owner)
        {
            Description = description;
            Owner = owner;
        }

        public bool IsValid() => !string.IsNullOrWhiteSpace(Description) && !string.IsNullOrWhiteSpace(Owner);
    }
}
