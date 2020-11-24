using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel
{
    public class FreshServiceDepartmentModel
    {
        public Departments[] Departments;
    }

    public class Departments
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
