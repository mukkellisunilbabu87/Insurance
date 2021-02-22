using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Common.DTO;

namespace Insurance.Common.Business
{
    public interface IInsuranceBusiness
    {
        Quote GetQuoteDetails();
        List<SearchResult> FindPeople(SearchFilter filter);
        List<AdditionalInsured> GetAdditionalInsuredForQuote(int personId, int quoteId);
        void AddAdditionalInsured(AdditionalInsured insurerInfo);
        void RemoveAdditionalInsured(int additionalInsuredId);
    }
}
