using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class FreshServiceModelExtensions
    {
        public static StatisticsDataModel PopulateTotalTicketsMoreThanSevenDays(this StatisticsDataModel model, FreshServiceTicketModel[] data)
        {
            var ticketsOpenMoreThanSevenDays = 0;
            if (model == null)
            {
                model = new StatisticsDataModel();
            }

            if (data == null)
            {
                return model;
            }

            foreach (var ticket in data)
            {
                if (ticket?.Tickets != null)
                    foreach (var individualTicket in ticket.Tickets)
                    {
                        if (individualTicket.Status != Constants.TicketWithPendingStatus && individualTicket.Status !=
                                                                                         Constants
                                                                                             .TicketWithResolvedStatus
                                                                                         && individualTicket.Status !=
                                                                                         Constants
                                                                                             .TicketWithClosedStatus
                                                                                         && individualTicket
                                                                                             .CreatedAt != null
                                                                                         && (DateTime.Now -
                                                                                             DateTime.Parse(
                                                                                                 individualTicket
                                                                                                     .CreatedAt
                                                                                                     .Substring(0, 10)))
                                                                                         .TotalDays > 7)
                        {
                            ticketsOpenMoreThanSevenDays += 1;
                        }
                    }
            }

            model.OpenMoreThanSevenDays = ticketsOpenMoreThanSevenDays;

            return model;
        }

        public static StatisticsDataModel PopulateTotalTicketsMoreThanThirtyDays(this StatisticsDataModel model, FreshServiceTicketModel[] data)
        {
            var ticketsOpenMoreThanThirtyDays = 0;

            if (model == null)
            {
                model = new StatisticsDataModel();
            }

            if (data == null)
            {
                return model;
            }


            foreach (var ticket in data)
            {
                if (ticket?.Tickets != null)
                    foreach (var individualTicket in ticket.Tickets)
                    {
                        if (individualTicket.Status != Constants.TicketWithPendingStatus && individualTicket.Status !=
                                                                                         Constants
                                                                                             .TicketWithResolvedStatus
                                                                                         && individualTicket.Status !=
                                                                                         Constants
                                                                                             .TicketWithClosedStatus
                                                                                         && individualTicket
                                                                                             .CreatedAt != null
                                                                                         && (DateTime.Now -
                                                                                             DateTime.Parse(
                                                                                                 individualTicket
                                                                                                     .CreatedAt
                                                                                                     .Substring(0, 10)))
                                                                                         .TotalDays > 30)
                        {
                            ticketsOpenMoreThanThirtyDays += 1;
                        }
                    }
            }

            model.OpenMoreThanThirtyDays = ticketsOpenMoreThanThirtyDays;

            return model;
        }

        public static MonthlyStatisticsDataModel PopulateTicketCountForTheMonth(this MonthlyStatisticsDataModel model, FreshServiceTicketModel[] data)
        {
            var ticketCountForTheMonth = 0;
            
            if (model == null)
            {
                model = new MonthlyStatisticsDataModel();
            }

            if (data == null)
            {
                return model;
            }

            foreach (var ticket in data)
            {
                if (ticket?.Tickets != null)
                    foreach (var individualTicket in ticket.Tickets)
                    {
                        if (individualTicket.CreatedAt != null
                            && (DateTime.Parse(individualTicket.CreatedAt.Substring(0, 10))).Month == DateTime.Now.Month
                        ) //Current month only
                        {
                            ticketCountForTheMonth += 1;
                        }
                    }
            }

            model.TicketCountForTheMonth = ticketCountForTheMonth;

            return model;
        }

        public static MonthlyStatisticsDataModel PopulateAverageTicketHandleTimeInMinutes(this MonthlyStatisticsDataModel model, FreshServiceTimeEntriesModel[] timeEntryData)
        {
            var ticketHandlingTimeStringList = new List<string>();
            if (model == null)
            {
                model = new MonthlyStatisticsDataModel();
            }

            if (timeEntryData == null)
            {
                return model;
            }
            
            foreach (var entry in timeEntryData)
            {
                if (entry?.Time_Entries != null)
                    foreach (var individualEntry in entry.Time_Entries)
                    {
                        if (!string.IsNullOrEmpty(individualEntry.TimeSpent) &&
                            (DateTime.Parse(individualEntry.CreatedAt.Substring(0, 10))).Month == DateTime.Now.Month
                        ) //Current month only
                        {
                            ticketHandlingTimeStringList.Add(individualEntry.TimeSpent);
                        }
                    }
            }

            var averageTicketHandlingTime = ticketHandlingTimeStringList.Select(TimeSpan.Parse)
                .Average(time => time.TotalMinutes);

            var decimalValue = (decimal?) averageTicketHandlingTime;
            model.AverageTicketHandleTimeInMinutes = Math.Round(decimalValue.GetValueOrDefault(), 10);

            return model;
        }

        public static MonthlyStatisticsDataModel PopulateTicketsResolvedAtLevelOne(this MonthlyStatisticsDataModel model, FreshServiceTicketModel[] data, string levelOneGroupIdentifierId)
        {
            var ticketsResolvedAtLevelOne = 0;
            
            if (model == null)
            {
                model = new MonthlyStatisticsDataModel();
            }

            if (data == null)
            {
                return model;
            }

            foreach (var ticket in data)
            {
                if (ticket?.Tickets != null)
                    foreach (var individualTicket in ticket.Tickets)
                    {
                        if (individualTicket.Status == Constants.TicketWithResolvedStatus
                            && individualTicket.UpdatedAt != null
                            && (DateTime.Parse(individualTicket.UpdatedAt.Substring(0, 10))).Month == DateTime.Now.Month
                            && individualTicket.GroupId == levelOneGroupIdentifierId)
                        {
                            ticketsResolvedAtLevelOne += 1;
                        }
                    }
            }

            model.TicketsResolvedByLevelOne = ticketsResolvedAtLevelOne;

            return model;
        }

        public static StatisticsDataModel PopulateTwoDayPercentageForTickets(this StatisticsDataModel model, FreshServiceTicketModel[] freshServiceTicketData)
        {
            var daysTicketIsOpen = new List<int>();
            
            if (model == null)
            {
                model = new StatisticsDataModel();
            }

            if (freshServiceTicketData == null)
            {
                return model;
            }

            foreach (var tickets in freshServiceTicketData)
            {
                if (tickets?.Tickets != null)
                    foreach (var individualTicket in tickets.Tickets)
                    {
                        if (individualTicket.Status != Constants.TicketWithPendingStatus && individualTicket.Status !=
                                                                                         Constants
                                                                                             .TicketWithClosedStatus
                                                                                         && individualTicket.Status !=
                                                                                         Constants
                                                                                             .TicketWithPendingStatus
                                                                                         && individualTicket.Status !=
                                                                                         Constants
                                                                                             .TicketWithResolvedStatus
                                                                                         && individualTicket
                                                                                             .CreatedAt != null)
                        {
                            daysTicketIsOpen.Add(
                                (DateTime.Now - DateTime.Parse(individualTicket.CreatedAt.Substring(0, 10))).Days);
                        }
                    }
            }

            var averageTicketOpenDays = daysTicketIsOpen.Average();

            var decimalValue = (decimal?)averageTicketOpenDays;
            model.TwoDayPercentage = Math.Round(decimalValue.GetValueOrDefault(), 10);

            return model;
        }
    }
}
