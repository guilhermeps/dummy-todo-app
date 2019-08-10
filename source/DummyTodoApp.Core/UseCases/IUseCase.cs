namespace DummyTodoApp.Core.UseCases
{
    public interface IUseCase<in I, out O>
    {
        O Execute(I input);
    }

    public interface IUseCase<in I>
    {
        void Execute(I input);
    }
}
