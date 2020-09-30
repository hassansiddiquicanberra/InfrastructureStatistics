using System.Linq;
using F1.Solutions.Service.Models;
using F1Solutions.InfrastructureStatictics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatictics.ApiCalls.Models;

namespace F1Solutions.InfrastructureStatictics.ApiCalls.Orchestrator
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

            model.TotalMspMissedCalls = (data.Calls.Where(x =>
                                           x.MissedCallReason != null && x.AnsweredAt == null
                                           && x.Number != null && x.Number.Name == regisNumber)).Count();

            return model;
        }
    }
}
