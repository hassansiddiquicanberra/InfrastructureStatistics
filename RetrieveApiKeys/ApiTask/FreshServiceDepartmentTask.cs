using System.Net.Http;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask
{
    public class FreshServiceDepartmentTask : BaseApiTask
    {
        public FreshServiceDepartmentTask()
        {
            Id = ConfigHelper.FreshServiceApiKey;
        }

        public override string Start(string ticketId = null, string url = null)
        {
            var freshServiceResponse = GetAllAsync(ConfigHelper.FreshServiceForDepartmentsUri, HttpMethod.Get);
            
            return freshServiceResponse.Result;
        }
    }
}
