using System.Collections.Generic;
using System.Linq;
using Insurance.Common.Business;
using Insurance.Common.DataAccess;
using Insurance.Common.DTO;

namespace Insurance.Business
{
    /// <summary>
    /// Insurance business class.
    /// </summary>
    public class InsuranceBusiness : IInsuranceBusiness
    {
        private readonly IInsuranceUOW insuranceUOW;

        // Class Constructor
        public InsuranceBusiness(IInsuranceUOW insuranceUOW)
        {
            this.insuranceUOW = insuranceUOW;
        }

        public void AddAdditionalInsured(AdditionalInsured insuredInfo)
        {
            if (!(insuranceUOW.RepositoryInstance.AdditionalInsuredList.Exists(c => c.PersonId == insuredInfo.PersonId && c.QuoteId == insuredInfo.QuoteId)))
            {
                insuranceUOW.RepositoryInstance.AdditionalInsuredList.Add(insuredInfo);
            }
        }

        public List<SearchResult> FindPeople(SearchFilter filter)
        {
            var searchResult = from p in insuranceUOW.RepositoryInstance.People
                               where (p.FirstName.ToLower().Contains(filter.FirstName.ToLower()) && p.LastName.ToLower().Contains(filter.LastName.ToLower()))
                                            && !insuranceUOW.RepositoryInstance.AdditionalInsuredList.Exists(a => a.PersonId == p.PersonId && a.QuoteId == filter.QuoteId)
                               select new SearchResult
                               {
                                   QuoteId = filter.QuoteId,
                                   PersonId = p.PersonId,
                                   FullName = p.Prefix + " " + p.FirstName + " " + p.LastName,
                                   DateOfBirth = p.DateOfBirth
                               };

            return searchResult.ToList();
        }

        public List<AdditionalInsured> GetAdditionalInsuredForQuote(int personId, int quoteId)
        {
            var additionalInsured = from a in insuranceUOW.RepositoryInstance.AdditionalInsuredList
                                    join p in insuranceUOW.RepositoryInstance.People on a.PersonId equals p.PersonId
                                    where a.QuoteId == a.QuoteId
                                    select new AdditionalInsured
                                    {
                                        AdditionalInsuredId = a.AdditionalInsuredId,
                                        PersonId = p.PersonId,
                                        QuoteId = quoteId,
                                        FirstName = p.Prefix + " " + p.FirstName + " " + p.LastName,
                                        DateOfBirth = p.DateOfBirth,
                                        InsuranceCoverage = (p.PersonId * 10)
                                    };

            return additionalInsured.ToList();
        }

        public Quote GetQuoteDetails()
        {
            if(insuranceUOW.RepositoryInstance.AdditionalInsuredList?.Count() > 0)
            {
                insuranceUOW.RepositoryInstance.AdditionalInsuredList = new List<AdditionalInsured>();
            }

            return this.insuranceUOW.RepositoryInstance.QuoteInfo;
        }

        public void RemoveAdditionalInsured(int additionalInsuredId)
        {
            insuranceUOW.RepositoryInstance.AdditionalInsuredList.RemoveAll(a => a.AdditionalInsuredId == additionalInsuredId);
        }
    }
}
