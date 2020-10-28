using System;
using System.Collections.Generic;
using System.Linq;
using F1Solutions.InfrastructureStatistics.ApiCalls.Models;
using F1Solutions.InfrastructureStatistics.ApiCalls.Utils;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.ModelExtensions
{
    public static class FreshServiceAgentModelExtensions
    {
        public static StatisticsAgentDataModel TotalTicketsResolvedToday(this StatisticsAgentDataModel model, FreshServiceTicketModel[] data)
        {
            int totalTicketsResolvedToday = 0;
            if (model == null)
            {
                model = new StatisticsAgentDataModel();
            }

            foreach (var tickets in data)
            {
                foreach (var ticket in tickets.Tickets)
                {
                    if (ticket.Status == Constants.TicketWithResolvedStatus && (DateTime.Parse(ticket.UpdatedAt.Substring(0, 10))).Day == DateTime.Now.Day)
                    {
                        totalTicketsResolvedToday++;
                    }
                }
            }

            model.TotalResolvedToday = totalTicketsResolvedToday;

            return model;
        }


        public static StatisticsAgentDataModel TotalTicketsOpen(this StatisticsAgentDataModel model, FreshServiceTicketModel[] data)
        {
            int totalTicketsStillOpen = 0;
            if (model == null)
            {
                model = new StatisticsAgentDataModel();
            }

            foreach (var tickets in data)
            {
                foreach (var ticket in tickets.Tickets)
                {
                    if (ticket.Status == Constants.TicketWithOpenStatus)
                    {
                        totalTicketsStillOpen++;
                    }
                }
            }

            model.TotalTicketsOpen = totalTicketsStillOpen;

            return model;
        }

        public static StatisticsAgentDataModel TotalResolvedLastSevenDays(this StatisticsAgentDataModel model, FreshServiceTicketModel[] data)
        {
            int totalResolvedLastSevenDays = 0;
            if (model == null)
            {
                model = new StatisticsAgentDataModel();
            }

            foreach (var tickets in data)
            {
                foreach (var ticket in tickets.Tickets)
                {
                    if (ticket.Status == Constants.TicketWithResolvedStatus && (DateTime.Now - DateTime.Parse(ticket.UpdatedAt.Substring(0, 10)))
                        .TotalDays < 8)
                    {
                        totalResolvedLastSevenDays++;
                    }
                }
            }

            model.TotalResolvedLastSevenDays = totalResolvedLastSevenDays;

            return model;
        }

        public static StatisticsAgentDataModel TotalResolvedLastThirtyDays(this StatisticsAgentDataModel model, FreshServiceTicketModel[] data)
        {
            int totalResolvedLastThirtyDays = 0;
            if (model == null)
            {
                model = new StatisticsAgentDataModel();
            }

            foreach (var tickets in data)
            {
                foreach (var ticket in tickets.Tickets)
                {
                    if (ticket.Status == Constants.TicketWithResolvedStatus && (DateTime.Now - DateTime.Parse(ticket.UpdatedAt.Substring(0, 10)))
                        .TotalDays < 31)
                    {
                        totalResolvedLastThirtyDays++;
                    }
                }
            }

            model.TotalResolvedLastSevenDays = totalResolvedLastThirtyDays;

            return model;
        }

        public static StatisticsAgentDataModel TotalNotRespondedToInTwoDays(this StatisticsAgentDataModel model,
            FreshServiceTicketModel[] data)
        {
            int totalNotRespondedInTwoDays = 0;
            if (model == null)
            {
                model = new StatisticsAgentDataModel();
            }

            foreach (var tickets in data)
            {
                foreach (var ticket in tickets.Tickets)
                {
                    if (ticket.Status == Constants.TicketWithOpenStatus && string.IsNullOrEmpty(ticket.UpdatedAt) && (DateTime.Now - DateTime.Parse(ticket.UpdatedAt.Substring(0, 10)))
                        .TotalDays < 3)
                    {
                        totalNotRespondedInTwoDays++;
                    }
                }
            }

            model.TotalResolvedLastSevenDays = totalNotRespondedInTwoDays;

            return model;
        }

        public static StatisticsAgentDataModel AverageTicketAgeLastSevenDays(this StatisticsAgentDataModel model, FreshServiceTicketModel[] data)
        {
            var listOfTicketDates = new List<double>();
            if (model == null)
            {
                model = new StatisticsAgentDataModel();
            }

            if (data == null)
            {
                return model;
            }

            foreach (var tickets in data)
            {
                foreach (var ticket in tickets.Tickets)
                {
                    if ((DateTime.Now - DateTime.Parse(ticket.CreatedAt.Substring(0, 10))).TotalDays < 8)
                    {
                        listOfTicketDates.Add((DateTime.Now - DateTime.Parse(ticket.CreatedAt.Substring(0, 10))).TotalDays);
                    }
                }
            }

            var averageTicketAgeLastSevenDays = listOfTicketDates.Average();

            var decimalValue = (decimal?)averageTicketAgeLastSevenDays;
            model.AverageTicketAgeLastSevenDays = Math.Round(decimalValue.GetValueOrDefault(), 10);

            return model;
        }

        public static StatisticsAgentDataModel AverageTicketResolutionTimeLastSevenDays(this StatisticsAgentDataModel model, FreshServiceTicketModel[] data)
        {
            var listOfTicketResolutionDays = new List<double>();
            if (model == null)
            {
                model = new StatisticsAgentDataModel();
            }

            if (data == null)
            {
                return model;
            }

            foreach (var tickets in data)
            {
                foreach (var ticket in tickets.Tickets)
                {
                    if (ticket.Status == Constants.TicketWithResolvedStatus && 
                        (DateTime.Now - DateTime.Parse(ticket.CreatedAt.Substring(0, 10))).TotalDays < 8)
                    {
                        //this ticket is resolved and is not before 7 days old
                        //get the days since it was resolved i.e updated date
                        listOfTicketResolutionDays.Add(
                            (DateTime.Now - DateTime.Parse(ticket.UpdatedAt.Substring(0, 10))).TotalDays);
                    }
                }
            }

            var averageTicketAgeLastSevenDays = listOfTicketResolutionDays.Average();

            var decimalValue = (decimal?)averageTicketAgeLastSevenDays;
            model.AverageTicketResolutionTimeLastSevenDays = Math.Round(decimalValue.GetValueOrDefault(), 10);

            return model;
        }
    }
}
