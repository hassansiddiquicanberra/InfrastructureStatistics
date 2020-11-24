using F1Solutions.InfrastructureStatistics.ApiCalls.ApiTask;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Helpers
{
    public static class ServiceCaller
    {
        public static string CallFreshServiceApi(FreshServiceApiTask freshServiceApiTask)
        {

            return freshServiceApiTask.Start();
        }

        public static string CallFreshServiceDepartment(FreshServiceDepartmentTask freshServiceDepartmentTask)
        {

            return freshServiceDepartmentTask.Start();
        }

        public static string CallFreshServiceRequester(FreshServiceRequesterTask freshServiceRequesterTask)
        {

            return freshServiceRequesterTask.Start();
        }
    }
}
