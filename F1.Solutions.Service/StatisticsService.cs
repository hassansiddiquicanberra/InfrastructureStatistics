using System;
using System.Linq;
using F1Solutions.InfrastructureStatistics.DataAccess;
using F1Solutions.InfrastructureStatistics.Services.Helpers;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.Services
{
    public class StatisticsService
    {
        static readonly StatisticsEntity DataAccessStatistics = new StatisticsEntity();

        public void SaveStatisticsValues(StatisticsDataModel model)
        {
            var statisticsValues = model.StatisticModelToDomain();

            DataAccessStatistics.Statistics.Add(statisticsValues);
            DataAccessStatistics.SaveChanges();
        }

        public void SaveMonthlyStatisticsValues(MonthlyStatisticsDataModel model)
        {
            var monthlyStatisticValues = model.MonthlyStatisticModelToDomain();

            DataAccessStatistics.MonthlyStatistics.Add(monthlyStatisticValues);
            DataAccessStatistics.SaveChanges();
        }

        public void SaveAgentStatisticsData(StatisticsAgentDataModel model)
        {
            var agentStatisticsValues = model.StatisticsAgentModelToDomain();

            DataAccessStatistics.Agents.Add(agentStatisticsValues);
            DataAccessStatistics.SaveChanges();
        }

        public void SaveOrganizationStatisticsData(StatisticsOrganisationDataModel model)
        {
            var organizationStatisticsValues = model.StatisticsOrganisationModelToDomain();

            DataAccessStatistics.Organisations.Add(organizationStatisticsValues);
            DataAccessStatistics.SaveChanges();
        }

        public void SaveTicket(TicketModel model)
        {
            //insert only if there is no matching ticket Id present in the database
            if (!DataAccessStatistics.Tickets.Any(x => x.TicketId == model.TicketId))
            {
                DataAccessStatistics.Tickets.Add(model.TicketModelToDomainObject());
                DataAccessStatistics.SaveChanges();
            }
        }

        public void SaveCall(CallModel model)
        {
            //insert only if there is no matching call Id present in the database
            if (!DataAccessStatistics.Calls.Any(x => x.CallId == model.CallId))
            {
                DataAccessStatistics.Calls.Add(model.CallModelToDomainObject());
                DataAccessStatistics.SaveChanges();
            }
        }

        public bool DoesAnyRecordExistForToday()
        {
            var currentDateTime = DateTime.Now;
            return EntityCalculationHelper.DoesValueExistForTodaysDate(DataAccessStatistics.MonthlyStatistics);
        }
    }
}
