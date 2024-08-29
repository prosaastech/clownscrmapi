namespace ClownsCRMAPI.CustomModels
{
    public class EventInfoModel
    {
        public int ContractEventInfoId { get; set; }

        public int? EventInfoEventType { get; set; }

        public int? EventInfoNumberOfChildren { get; set; }

        public DateTime? EventInfoEventDate { get; set; }

        public string EventInfoPartyStartTime { get; set; }

        public string EventInfoPartyEndTime { get; set; }

        public int? EventInfoTeamAssigned { get; set; }

        public string EventInfoStartClownHour { get; set; }

        public string EventInfoEndClownHour { get; set; }

        public string? EventInfoEventAddress { get; set; }

        public string? EventInfoEventCity { get; set; }

        public int? EventInfoEventZip { get; set; }

        public int? EventInfoEventState { get; set; }

        public int? EventInfoVenue { get; set; }

        public string? EventInfoVenueDescription { get; set; }

        public int? ContractId { get; set; }

        public int? CustomerId { get; set; }

        public DateTime selectedDate { get; set; }
    }
}
