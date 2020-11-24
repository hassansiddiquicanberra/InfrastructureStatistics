using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel
{
    public class FreshServiceRequesterModel
    {
        public Requesters[] Requesters;
    }

    public class Requesters
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("primary_email")]
        public string PrimaryEmail { get; set; }
    }
}
