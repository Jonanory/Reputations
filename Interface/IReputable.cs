
namespace Reputations
{
    public interface IReputable
    {
        Attributes InternalAttributes { get; set; }
        Attributes ExternalAttributes { get; set; }
        float ReputationHelp { get; set; }
    }
}
