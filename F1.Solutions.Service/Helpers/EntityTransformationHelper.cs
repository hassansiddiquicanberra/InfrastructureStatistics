using F1Solutions.InfrastructureStatistics.DataAccess;
using F1Solutions.InfrastructureStatistics.Services.Models;

namespace F1Solutions.InfrastructureStatistics.Services.Helpers
{
    public static class EntityTransformationHelper
    {
        public static Ticket TicketModelToDomainObject(this TicketModel model)
        {
            return new Ticket()
            {
                TicketId = model.TicketId,
                UpdatedAt = model.UpdatedAt,
                CreatedAt = model.CreatedAt,
                Status = model.Status,
                Requester = model.Requester,
                DepartmentName = model.DepartmentName,
                DueBy = model.DueBy,
                Description = model.Description,
                TicketType = model.TicketType,
            };
        }

        public static Call CallModelToDomainObject(this CallModel model)
        {
            return new Call()
            {
                CallId = model.CallId,
                Status = model.Status,
                MissedCallReason = model.MissedCallReason,
                AnsweredAt = model.AnsweredAt,
                AssignedTo = model.AssignedTo,
                AssignedToEmail = model.AssignedToEmail,
                Direction = model.Direction,
                Duration = model.Duration,
                EndedAt = model.EndedAt,
                StartedAt = model.StartedAt,
                UserName = model.UserName,
            };
        }
    }
}
