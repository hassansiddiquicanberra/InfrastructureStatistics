using System.Linq;
using F1Solutions.InfrastructureStatistics.DataAccess;
using F1Solutions.InfrastructureStatistics.Services.Helpers;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.Services
{
    public class StatisticsService
    {
        static readonly StatisticsEntity DataAccessStatistics = new StatisticsEntity();

        public void SaveTicket(TicketModel ticketModel)
        {
            var existingTicketRecord = DataAccessStatistics.Tickets.SingleOrDefault(x => x.TicketId == ticketModel.TicketId);

            if(existingTicketRecord == null && !DataAccessStatistics.Tickets.Any(x => x.TicketId == ticketModel.TicketId))
            {
                DataAccessStatistics.Tickets.Add(ticketModel.TicketModelToDomainObject());
                
            }
            else if (existingTicketRecord != null)//update
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

            else if (existingCallRecord != null)//update
            {
                DataAccessStatistics.Entry(existingCallRecord).CurrentValues.SetValues(callModel);
            }

            DataAccessStatistics.SaveChanges();
        }
    }
}
