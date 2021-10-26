using System.Threading.Tasks;

namespace popo.Handlers
{
    public interface IHandler<TRequest, TResult>
    {
        TResult Handle(TRequest request);
    }
}
