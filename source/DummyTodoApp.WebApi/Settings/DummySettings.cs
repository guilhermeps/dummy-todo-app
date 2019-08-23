namespace DummyTodoApp.WebApi.Settings
{
    public sealed class DummySettings : IValidatable
    {
        public string SomeDummyProperty { get; set; }
        public bool IsOpenToGetTodos { get; set; }

        public void Validate() 
        {
            if (string.IsNullOrWhiteSpace(SomeDummyProperty))
                throw new System.Exception("DummyProperty must not be null or empty.");        
        }
    }
}