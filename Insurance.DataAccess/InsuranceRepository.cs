using System;
using System.Collections.Generic;
using Insurance.Common.DataAccess;
using Insurance.Common.DTO;

namespace Insurance.DataAccess
{
    public class InsuranceRepository : IInsuranceRepository
    {
        private static List<AdditionalInsured> additionalInsureds = new List<AdditionalInsured>();
        public Quote QuoteInfo { get; private set; }
        public List<Person> People { get; private set; }
        public List<AdditionalInsured> AdditionalInsuredList
        {
            get
            {
                return additionalInsureds;
            }
            set
            {
                additionalInsureds = value;
            }
        }

        public InsuranceRepository()
        {
            InitializeData();
        }

        private void InitializeData()
        {
            this.QuoteInfo = InsuranceRepository.GetDummyQuoteData();
            this.People = InsuranceRepository.GetDummyPersonData();
            this.AdditionalInsuredList = additionalInsureds ?? new List<AdditionalInsured>();
        }

        private static Quote GetDummyQuoteData()
        {
            return new Quote()
            {
                QuoteId = 1,
                Number = "Q92348",
                Applicant = "James Feather LLC",
                Date = DateTime.Now.ToString("dd/MM/yyyy"),
                EffectiveDate = DateTime.Now.AddDays(6).ToString("dd/MM/yyyy"),
                PremiumOption = "Basic",
                Status = false
            };
        }

        private static List<Person> GetDummyPersonData()
        {
            return new List<Person>
            {
                new Person() { PersonId = 1, Prefix = "Mr.", FirstName = "James", LastName = "Feather", DateOfBirth = "03/01/1980" },
                new Person() { PersonId = 2, Prefix = "Mr.", FirstName = "John", LastName = "Krakow", DateOfBirth = "04/02/1981" },
                new Person() { PersonId = 3, Prefix = "Mr.", FirstName = "Red", LastName = "Hemmington", DateOfBirth = "08/10/1982" }
            };
        }
    }
}
