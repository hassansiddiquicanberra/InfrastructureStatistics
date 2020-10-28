using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class FreshServiceOrganistionModelExtensions
    {
        public static StatisticsOrganisationDataModel TotalTicketsOpen(this StatisticsOrganisationDataModel model, FreshServiceTicketModel[] data)
        {
            return new StatisticsOrganisationDataModel();
        }
    }
}
