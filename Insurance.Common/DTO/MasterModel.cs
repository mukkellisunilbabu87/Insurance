using System.Collections.Generic;

namespace Insurance.Common.DTO
{
    public class MasterModel
    {
        public Quote Quote { get; set; }
        public List<SearchResult> SearchResult { get; set; }
        public List<AdditionalInsured> AdditionalInsureds { get; set; }
    }
}
