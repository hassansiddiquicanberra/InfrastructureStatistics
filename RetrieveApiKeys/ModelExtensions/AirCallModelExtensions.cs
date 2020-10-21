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

            model.TotalMspMissedCalls = FilterCallsByPhoneTypeName(ConfigHelper.MspNumber, airCallData);

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

            model.TotalRegisMissedCalls = FilterCallsByPhoneTypeName(ConfigHelper.RegisNumber, airCallData);

            return model;
        }

        private static int FilterCallsByPhoneTypeName(string phoneTypeName, AirCallModel[] airCallData)
        {
            var totalCalls = 0;
            if (airCallData != null)
            {
                foreach (var allCalls in airCallData)
                {
                    if (allCalls.Calls != null)
                    {
                        foreach (var individualCall in allCalls.Calls)
                        {
                            if (individualCall.MissedCallReason != null
                                && individualCall.AnsweredAt == null
                                && individualCall.Number != null
                                && individualCall.Number.Name == phoneTypeName)
                            {
                                totalCalls += 1;
                            }
                        }
                    }
                }
            }

            return totalCalls;
        }
    }
}
