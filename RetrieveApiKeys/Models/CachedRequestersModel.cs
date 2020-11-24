namespace F1Solutions.InfrastructureStatistics.ApiCalls.Models
{
    public class CachedRequestersModel
    {
        public CachedRequestersModel(Requester requester)
        {
            CachedRequester = requester;
        }
        public Requester CachedRequester { get; set; }
    }

    public class Requester
    {
        public string Id { get; set; }
        public string PrimaryEmail { get; set; }
    }
}