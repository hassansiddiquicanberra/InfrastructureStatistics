namespace F1Solutions.InfrastructureStatistics.ApiCalls.Models
{
    public class CachedAgentsModel
    {
        public CachedAgentsModel(Agent agent)
        {
            CachedAgent = agent;
        }
        public Agent CachedAgent { get; set; }
    }

    public class Agent
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}