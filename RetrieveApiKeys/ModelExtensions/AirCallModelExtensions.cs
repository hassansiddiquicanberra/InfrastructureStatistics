using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class AirCallModelExtensions
    {
        public static StatisticsDataModel PopulateTotalMspMissedCalls(this StatisticsDataModel model, AirCallModel[] airCallData)
        {
            if (model == null)
            {
                model = new StatisticsDataModel();
            }

            if (airCallData == null)
            {
                return model;
            }

            model.TotalMspMissedCalls = FilterCallsByName(ConfigHelper.MspNumber, airCallData);

            return model;
        }

        public static StatisticsDataModel PopulateTotalRegisMissedCalls(this StatisticsDataModel model, AirCallModel[] airCallData)
        {
            if (model == null)
            {
                model = new StatisticsDataModel();
            }

            if (airCallData == null)
            {
                return model;
            }

            model.TotalRegisMissedCalls = FilterCallsByName(ConfigHelper.RegisNumber, airCallData);

            return model;
        }

        private static int FilterCallsByName(string phoneTypeName, AirCallModel[] data)
        {
            var total = 0;
            foreach (var calls in data)
            {
                foreach (var individualCall in calls.Calls)
                {
                    if (individualCall.MissedCallReason != null
                        && individualCall.AnsweredAt == null
                        && individualCall.Number != null
                        && individualCall.Number.Name == phoneTypeName)
                    {
                        total += 1;
                    }
                }
            }

            return total;
        }
    }
}
