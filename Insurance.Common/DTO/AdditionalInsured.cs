namespace Insurance.Common.DTO
{
    public class AdditionalInsured
    {
        public int AdditionalInsuredId { get; set; }
        public int QuoteId { get; set; }
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string DateOfBirth { get; set; }
        public int InsuranceCoverage { get; set; }
    }
}
