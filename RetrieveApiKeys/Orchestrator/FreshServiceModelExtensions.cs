using System;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.Orchestrator
{
    public static class FreshServiceModelExtensions
    {
        public static DataModel PopulateTotalTicketsMoreThanSevenDays(this DataModel model, FreshServiceTicketModel data)
        {
            var ticketsOpenMoreThanSevenDays = 0;
            if (model == null)
            {
                model = new DataModel();
            }

            if (data == null)
            {
                return model;
            }

            var ticketsNotClosedOrDeferredOrResolved = data.Tickets
                .Where(x => x.Status != Constants.PendingStatus && x.Status != Constants.ResolvedStatus
                                                               && x.Status != Constants.ClosedStatus);

            if (ticketsNotClosedOrDeferredOrResolved.Any())
            {
                ticketsOpenMoreThanSevenDays = (ticketsNotClosedOrDeferredOrResolved.Where(x =>
                      x.CreatedAt != null &&
                      (DateTime.Now - DateTime.Parse(x.CreatedAt.Substring(0, 10)))
                                            .TotalDays > 7)).Count();
            }

            model.OpenMoreThanSevenDays = ticketsOpenMoreThanSevenDays;

            return model;
        }

        public static DataModel PopulateTotalTicketsMoreThanThirtyDays(this DataModel model, FreshServiceTicketModel data)
        {
            var ticketsOpenMoreThanThirtyDays = 0;

            if (model == null)
            {
                model = new DataModel();
            }

            if (data == null)
            {
                return model;
            }

            var ticketsNotClosedOrDeferredOrResolved = data.Tickets
                                .Where(x => x.Status != Constants.PendingStatus
                                         && x.Status != Constants.ResolvedStatus
                                         && x.Status != Constants.ClosedStatus);

            if (ticketsNotClosedOrDeferredOrResolved.Any())
            {
                ticketsOpenMoreThanThirtyDays = (ticketsNotClosedOrDeferredOrResolved.Where(x =>
                    x.CreatedAt != null &&
                    (DateTime.Now - DateTime.Parse(x.CreatedAt.Substring(0, 10)))
                    .TotalDays > 30)).Count();
            }

            model.OpenMoreThanThirtyDays = ticketsOpenMoreThanThirtyDays;

            return model;
        }

        public static DataModel PopulateTicketsResolvedAtLevelOne(this DataModel model, FreshServiceTicketModel data)
        {
            var ticketsResolvedAtLevelOne = 0;
            var currentDate = DateTime.Now;
            if (model == null)
            {
                model = new DataModel();
            }

            if (data == null)
            {
                return model;
            }

            var resolvedTickets = data.Tickets
                .Where(x => x.Status == Constants.ResolvedStatus &&
                      x.UpdatedAt != null &&
                      (DateTime.Parse(x.UpdatedAt.Substring(0, 10))).Month == DateTime.Now.Month);

            if (resolvedTickets.Any())
            {
                ticketsResolvedAtLevelOne = (resolvedTickets.Where(x =>
                    x.GroupId == data.LevelOneGroup)).Count();

            }

            model.TicketsResolvedByLevelOne = ticketsResolvedAtLevelOne;

            return model;
        }

        public static DataModel PopulateTicketCountForTheMonth(this DataModel model, FreshServiceTicketModel data)
        {
            var ticketCountForTheMonth = 0;
            if (model == null)
            {
                model = new DataModel();
            }

            if (data == null)
            {
                return model;
            }

            ticketCountForTheMonth = (data.Tickets.Where(x=> (DateTime.Parse(x.CreatedAt.Substring(0, 10))).Month == DateTime.Now.Month)).Count();

            model.TicketCountForTheMonth = ticketCountForTheMonth;

            return model;
        }

        public static DataModel PopulateAverageTicketHandleTimeInMinutes(this DataModel model, FreshServiceTicketModel data)
        {
            var averageTicketHandlingTime = 0.0m;
            if (model == null)
            {
                model = new DataModel();
            }

            if (data == null)
            {
                return model;
            }
            //TODO replace with actual field - assuming that minutes handle ticket is available in the field email config id

            var currentMonthTickets = (data.Tickets.Where(x =>
                (DateTime.Parse(x.CreatedAt.Substring(0, 10))).Month == DateTime.Now.Month));
             
            //averageTicketHandlingTime = currentMonthTickets.Average(x => (Convert.ToDecimal(x.EmailConfigId)));

            model.AverageTicketHandleTimeInMinutes =  averageTicketHandlingTime;

            return model;
        }

    }
}
