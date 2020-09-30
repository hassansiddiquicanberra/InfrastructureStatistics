using System;
using System.Linq;
using F1.Solutions.Service.Models;
using RetrieveApiKeys.Helpers;
using RetrieveApiKeys.Models;

namespace RetrieveApiKeys.Orchestrator
{
    public static class FreshServiceModelExtensions
    {

        public static DataModel PopulateTotalTicketsMoreThanSevenDays(this DataModel model, FreshServiceTicketModel[] data)
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

            var ticketsNotClosedOrDeferredOrResolved = data
                .Where(x => x.Status != Constants.PendingStatus && x.Status != Constants.ResolvedStatus
                                                               && x.Status != Constants.ClosedStatus);

            if (ticketsNotClosedOrDeferredOrResolved.Any())
            {
                ticketsOpenMoreThanSevenDays = (ticketsNotClosedOrDeferredOrResolved.Where(x =>
                      x.CreatedAt != null &&
                      (DateTime.Now - DateTime.Parse(x.CreatedAt.Substring(0, 10)))
                                            .TotalDays > 7)).Count();

                return model;
            }

            model.OpenMoreThanSevenDays = ticketsOpenMoreThanSevenDays;

            return model;
        }

        public static DataModel PopulateTotalTicketsMoreThanThirtyDays(this DataModel model, FreshServiceTicketModel[] data)
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

            var ticketsNotClosedOrDeferredOrResolved = data
                                .Where(x => x.Status != Constants.PendingStatus 
                                         && x.Status != Constants.ResolvedStatus
                                         && x.Status != Constants.ClosedStatus);

            if (ticketsNotClosedOrDeferredOrResolved.Any())
            {
                ticketsOpenMoreThanThirtyDays = (ticketsNotClosedOrDeferredOrResolved.Where(x =>
                    x.CreatedAt != null &&
                    (DateTime.Now - DateTime.Parse(x.CreatedAt.Substring(0, 10)))
                    .TotalDays > 7)).Count();

                return model;
            }

            model.OpenMoreThanSevenDays = ticketsOpenMoreThanThirtyDays;

            return model;
        }
    }
}
