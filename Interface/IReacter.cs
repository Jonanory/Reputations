
namespace Reputations
{
    public interface IReacter
    {
        Reputation Reputation { get; }
        void ReactTo(IReputable reputable, Action action);
    }
}