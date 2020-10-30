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

        public void SaveTicket(TicketModel ticketModel)
        {
            var existingTicketRecord = DataAccessStatistics.Tickets.SingleOrDefault(x => x.TicketId == ticketModel.TicketId);

            if(existingTicketRecord == null && !DataAccessStatistics.Tickets.Any(x => x.TicketId == ticketModel.TicketId))
            {
                DataAccessStatistics.Tickets.Add(ticketModel.TicketModelToDomainObject());
                
            }
            //update
            else if (existingTicketRecord != null)
            {
                DataAccessStatistics.Entry(existingTicketRecord).CurrentValues.SetValues(ticketModel);
            }

            DataAccessStatistics.SaveChanges();
        }

        public void SaveCall(CallModel callModel)
        {
            var existingCallRecord = DataAccessStatistics.Calls.SingleOrDefault(x => x.CallId == callModel.CallId);
            if(existingCallRecord == null && !DataAccessStatistics.Calls.Any(x => x.CallId == callModel.CallId))
            {
                DataAccessStatistics.Calls.Add(callModel.CallModelToDomainObject());
            }

            //update
            else if (existingCallRecord != null)
            {
                DataAccessStatistics.Entry(existingCallRecord).CurrentValues.SetValues(callModel);
            }

            DataAccessStatistics.SaveChanges();
        }

        public bool DoesAnyRecordExistForToday()
        {
            var currentDateTime = DateTime.Now;
            return EntityCalculationHelper.DoesValueExistForTodaysDate(DataAccessStatistics.MonthlyStatistics);
        }
    }
}
