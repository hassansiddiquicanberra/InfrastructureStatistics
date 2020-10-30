using Newtonsoft.Json;

namespace F1Solutions.InfrastructureStatistics.ApiCalls.JsonModel
{
    public class AirCallModel
    {
        public Calls[] Calls;

        public Meta Meta;
    }

    public class Meta
    {
        [JsonProperty("count")]
        public string Count { get; set; }
        [JsonProperty("total")]
        public string Total { get; set; }
        [JsonProperty("current_page")]
        public string CurrentPage { get; set; }
        [JsonProperty("per_page")]
        public string PerPage { get; set; }
        [JsonProperty("next_page_link")]
        public string NextPageLink { get; set; }
        [JsonProperty("previous_page_link")]
        public string PreviousPageLink { get; set; }
    }

    public class Calls
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("missed_call_reason")]
        public string MissedCallReason { get; set; }
        [JsonProperty("started_at")]
        public string StartedAt { get; set; }
        [JsonProperty("answered_at")]
        public string AnsweredAt { get; set; }
        [JsonProperty("ended_at")]
        public string EndedAt { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("voicemail")]
        public string Voicemail { get; set; }
        [JsonProperty("recording")]
        public string Recording { get; set; }
        [JsonProperty("asset")]
        public string Asset { get; set; }
        [JsonProperty("raw_digits")]
        public string RawDigits { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("contact")]
        public Contact Contact { get; set; }
        [JsonProperty("archived")]
        public string Archived { get; set; }
        [JsonProperty("assigned_to")]
        public Assigned_To AssignedTo { get; set; }
        [JsonProperty("tags")]
        public Tags[] Tags { get; set; }
        [JsonProperty("transferred_to")]
        public Transferred_To TransferredTo { get; set; }
        [JsonProperty("teams")]
        public Teams[] Teams { get; set; }
        [JsonProperty("number")]
        public Number Number { get; set; }
        [JsonProperty("cost")]
        public string Cost { get; set; }
        [JsonProperty("comments")]
        public string[] Comments { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("available")]
        public string Available { get; set; }
        [JsonProperty("availability_status")]
        public string AvailabilityStatus { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }

    public class Contact
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [JsonProperty("information")]
        public string Information { get; set; }
        [JsonProperty("is_shared")]
        public string IsShared { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        [JsonProperty("emails")]
        public Emails[] Emails { get; set; }
        [JsonProperty("phone_numbers")]
        public Phone_Numbers[] PhoneNumbers { get; set; }
    }

    public class Emails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Phone_Numbers
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Tags
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("tagged_by")]
        public Tagged_By TaggedBy { get; set; }
    }

    public class Tagged_By
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("available")]
        public string Available { get; set; }
        [JsonProperty("availability_status")]
        public string AvailabilityStatus { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }

    public class Number
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("digits")]
        public string Digits { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }
        [JsonProperty("open")]
        public string Open { get; set; }
        [JsonProperty("availability_status")]
        public string AvailabilityStatus { get; set; }
        [JsonProperty("is_ivr")]
        public string IsIvr { get; set; }
        [JsonProperty("live_recording_activated")]
        public string LiveRecordingActivated { get; set; }
        [JsonProperty("messages")]
        public Messages Messages { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }

    public class Messages
    {
        [JsonProperty("welcome")]
        public string Welcome { get; set; }
        [JsonProperty("waiting")]
        public string Waiting { get; set; }
        [JsonProperty("ivr")]
        public string Ivr { get; set; }
        [JsonProperty("voicemail")]
        public string Voicemail { get; set; }
        [JsonProperty("closed")]
        public string Closed { get; set; }
        [JsonProperty("callback_later")]
        public string CallbackLater { get; set; }
        [JsonProperty("unanswered_call")]
        public string UnansweredCall { get; set; }
        [JsonProperty("after_hours")]
        public string AfterHours { get; set; }
        [JsonProperty("ringing_tone")]
        public string RingingTone { get; set; }
    }
    public class Transferred_To
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("available")]
        public string Available { get; set; }
        [JsonProperty("availability_status")]
        public string AvailabilityStatus { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }

    public class Assigned_To
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("available")]
        public string Available { get; set; }
        [JsonProperty("availability_status")]
        public string AvailabilityStatus { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }
    }

    public class Teams
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }


}
