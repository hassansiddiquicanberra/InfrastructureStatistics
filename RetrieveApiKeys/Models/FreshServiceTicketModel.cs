using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatictics.ApiCalls.Models
{
    public class FreshServiceTicketModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("requester_id")]
        public string RequesterId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("spam")]
        public string Spam { get; set; }
        [JsonProperty("deleted")]
        public string Deleted { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("display_id")]
        public string DisplayId { get; set; }
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
        [JsonProperty("group_id")]
        public string GroupId { get; set; }
        [JsonProperty("due_by")]
        public string DueBy { get; set; }
        [JsonProperty("frDueBy")]
        public string FrDueBy { get; set; }
        [JsonProperty("isescalated")]
        public string IsEscalated { get; set; }
        [JsonProperty("priority")]
        public string Priority { get; set; }
        [JsonProperty("fr_escalated")]
        public string FrEscalated { get; set; }
        [JsonProperty("to_email")]
        public string ToEmail { get; set; }
        [JsonProperty("email_config_id")]
        public string EmailConfigId { get; set; }
        [JsonProperty("cc_email")]
        public Cc_Email CcEmail { get; set; }
        [JsonProperty("ticket_type")]
        public string TicketType { get; set; }
        [JsonProperty("urgency")]
        public string Urgency { get; set; }
        [JsonProperty("impact")]
        public string Impact { get; set; }
        [JsonProperty("department_id_value")]
        public string DepartmentIdValue { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("sub_category")]
        public string SubCategory { get; set; }
        [JsonProperty("item_category")]
        public string ItemCategory { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("description_html")]
        public string DescriptionHtml { get; set; }
        [JsonProperty("status_name")]
        public string StatusName { get; set; }
        [JsonProperty("requester_status_name")]
        public string RequesterStatusName { get; set; }
        [JsonProperty("priority_name")]
        public string PriorityName { get; set; }
        [JsonProperty("source_name")]
        public string SourceName { get; set; }
        [JsonProperty("requester_name")]
        public string RequesterName { get; set; }
        [JsonProperty("responder_name")]
        public string ResponderName { get; set; }
        [JsonProperty("to_emails")]
        public string[] ToEmails { get; set; }
        [JsonProperty("department_name")]
        public string DepartmentName { get; set; }
        [JsonProperty("assoc_problem_id")]
        public string AssocProblemId { get; set; }
        [JsonProperty("assoc_change_id")]
        public string AssocChangeId { get; set; }
        [JsonProperty("assoc_change_cause_id")]
        public string AssocChangeCauseId { get; set; }
        [JsonProperty("assoc_asset_id")]
        public string AssocAssetId { get; set; }
        [JsonProperty("approval_status")]
        public string ApprovalStatus { get; set; }
        [JsonProperty("approval_status_name")]
        public string ApprovalStatusName { get; set; }
        [JsonProperty("urgency_name")]
        public string UrgencyName { get; set; }
        [JsonProperty("impact_name")]
        public string ImpactName { get; set; }
        [JsonProperty("custom_field")]
        public Custom_Field CustomField { get; set; }
    }

    public class Cc_Email
    {
        [JsonProperty("cc_emails")]
        public string[] CcEmails { get; set; }
        [JsonProperty("fwd_emails")]
        public string[] FwdEmails { get; set; }
    }

    public class Custom_Field
    {
        [JsonProperty("permanently_closed_55545")]
        public string PermanentlyClosed55545 { get; set; }
        [JsonProperty("due_date_55545")]
        public string DueDate55545 { get; set; }
        [JsonProperty("developer_change_required_55545")]
        public string DeveloperChangeRequired55545 { get; set; }
    }

}
