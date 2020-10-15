using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Models
{
    public class FreshServiceTimeEntriesModel
    {
        public Time_Entries[] Time_Entries;
    }

    public class Time_Entries
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("start_time")]
        public string StartTime { get; set; }
        [JsonProperty("timer_running")]
        public string TimerRunning { get; set; }
        [JsonProperty("billable")]
        public string billable { get; set; }
        [JsonProperty("time_spent")]
        public string TimeSpent { get; set; }
        [JsonProperty("executed_at")]
        public string ExecutedAt { get; set; }
        [JsonProperty("task_id")]
        public string TaskId { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("agent_id")]
        public string AgentId { get; set; }
    }
}