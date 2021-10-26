namespace Energy.Handlers
{
    public interface IHandler<TRequest, TResult>
    {
        TResult Handle(TRequest request);
    }
}
