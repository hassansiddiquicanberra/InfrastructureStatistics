using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Models
{
    public class FreshServiceAgentGroupModel
    {
        public Groups[] Groups;
    }

    public class Groups
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("escalate_to")]
        public string EscalateTo { get; set; }
        [JsonProperty("unassigned_for")]
        public string UnassignedFor { get; set; }
        [JsonProperty("agent_ids")]
        public string[] AgentIds { get; set; }
        [JsonProperty("members")]
        public string[] Members { get; set; }
        [JsonProperty("business_hours_id")]
        public string BusinessHoursId { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
    }
}


