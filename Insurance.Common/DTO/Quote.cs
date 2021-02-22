namespace Insurance.Common.DTO
{
    public class Quote
    {
        public int QuoteId { get; set; }
        public string Number { get; set; }
        public bool Status { get; set; }
        public string Applicant { get; set; }
        public string Date { get; set; }
        public string EffectiveDate { get; set; }
        public string PremiumOption { get; set; }
    }
}
