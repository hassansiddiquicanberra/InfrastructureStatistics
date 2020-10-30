using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Helpers;
using F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class AirCallModelExtensions
    {
        public static List<CallModel> PopulateCallData(AirCallModel[] airCallData)
        {
            var listOfCalls = new List<CallModel>();

            if (airCallData != null)
            {
                foreach (var allCalls in airCallData)
                {
                    if (allCalls.Calls != null && allCalls.Calls.Any())
                    {
                        foreach (var individualCall in allCalls.Calls)
                        {
                            var model = new CallModel
                            {
                                CallId = Convert.ToInt32(individualCall.Id),
                                Status = individualCall.Status,
                                Duration = individualCall.Duration,
                                Direction = individualCall.Direction,
                                MissedCallReason = individualCall.MissedCallReason,
                                StartedAt = TransformationHelper.UnixTimeStampToDateTime(Convert.ToDouble(individualCall.StartedAt)),
                                AnsweredAt = TransformationHelper.UnixTimeStampToDateTime(Convert.ToDouble(individualCall.AnsweredAt)),
                                EndedAt = TransformationHelper.UnixTimeStampToDateTime(Convert.ToDouble(individualCall.EndedAt))
                            };

                            if (individualCall.AssignedTo != null)
                            {
                                if (individualCall.AssignedTo.Name != null)
                                {
                                    model.AssignedTo = individualCall.AssignedTo.Name;
                                }

                                if (individualCall.AssignedTo.Email != null)
                                {
                                    model.AssignedToEmail = individualCall.AssignedTo.Email;
                                }
                            }

                            if (individualCall.User?.Name != null)
                            {
                                model.UserName = individualCall.User.Name;
                            }

                            listOfCalls.Add(model);
                        }
                    }
                }
            }

            listOfCalls.First().UserName = "Hello";

            return listOfCalls;
        }
    }
}
