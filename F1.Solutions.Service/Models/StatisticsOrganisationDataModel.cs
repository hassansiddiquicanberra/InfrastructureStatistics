﻿using System;

namespace F1Solutions.InfrastructureStatistics.Services.Models
{
    public class StatisticsOrganisationDataModel
    {
        public int OrganisationId { get; set; }
        public string OrganistionName { get; set; }
        public DateTime DateRecorded { get; set; }
        public int TotalOpen { get; set; }
        public int TotalOlderThanSevenDays { get; set; }
        public int TotalOlderThanThirtyDays { get; set; }
    }
}
