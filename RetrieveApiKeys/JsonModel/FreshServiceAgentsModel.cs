using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel
{
    public class FreshServiceAgentsModel
    {
        public Agents[] Agents;
    }

    public class Agents
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
