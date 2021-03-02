using System.Collections.Generic;
using Insurance.Common.DTO;

namespace Insurance.Common.DataAccess
{
    /// <summary>
    /// Interface for insurance repository
    /// </summary>
    public interface IInsuranceRepository
    {
        Quote QuoteInfo { get; }
        List<Person> People { get; }
        List<AdditionalInsured> AdditionalInsuredList { get; set; }
    }
}
