
namespace Reputations
{
    public interface IReputable
    {
        Attributes InternalAttributes { get; set; }
        Attributes ExternalAttributes { get; set; }
        float ReputationHelp { get; set; }
    }

    public class ReputableTest : IReputable
    {
        public Attributes InternalAttributes { get; set; }
        public Attributes ExternalAttributes { get; set; }
        public float ReputationHelp { get; set; }
    }
}
