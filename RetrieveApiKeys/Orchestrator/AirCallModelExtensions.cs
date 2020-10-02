using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator
{
    public static class AirCallModelExtensions
    {
        public static DataModel PopulateTotalMspMissedCalls(this DataModel model, AirCallModel data)
        {
            if (model == null)
            {
                model = new DataModel();
            }

            if (data == null)
            {
                return model;
            }

            var mspNumber = ConfigHelper.MspNumber;

            model.TotalMspMissedCalls = (data.Calls.Where(x =>
                                           x.MissedCallReason != null && x.AnsweredAt == null
                                           && x.Number != null && x.Number.Name == mspNumber)).Count();

            return model;
        }

        public static DataModel PopulateTotalRegisMissedCalls(this DataModel model, AirCallModel data)
        {
            if (model == null)
            {
                model = new DataModel();
            }

            if (data == null)
            {
                return model;
            }

            var regisNumber = ConfigHelper.RegisNumber;

            model.TotalRegisMissedCalls = (data.Calls.Where(x =>
                                           x.MissedCallReason != null && x.AnsweredAt == null
                                           && x.Number != null && x.Number.Name == regisNumber)).Count();

            return model;
        }
    }
}
