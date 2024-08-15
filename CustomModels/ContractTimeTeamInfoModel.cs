namespace ClownsCRMAPI.CustomModels
{
    public class ContractTimeTeamInfoModel
    {
        public int TeamId { get; set; }

        public int TimeSlotId { get; set; }

        public int? ContractId { get; set; }

        public DateTime Date { get; set; }
    }
}
