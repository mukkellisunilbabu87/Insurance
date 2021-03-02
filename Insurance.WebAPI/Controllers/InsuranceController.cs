using System.Net;
using System.Net.Http;
using System.Web.Http;
using Insurance.Common.Business;
using Insurance.Common.DTO;

namespace Insurance.WebAPI.Controllers
{
    /// <summary>
    /// Insurance controller class.
    /// </summary>
    [RoutePrefix("api/insurance")]
    public class InsuranceController : ApiController
    {
        private readonly IInsuranceBusiness _insuranceBusiness;

        public InsuranceController(IInsuranceBusiness insuranceBusiness)
        {
            this._insuranceBusiness = insuranceBusiness;
        }

        [HttpGet]
        [Route("LoadQuote")]
        public HttpResponseMessage GetQuote()
        {
            HttpResponseMessage responseMessage;

            var quotes = this._insuranceBusiness.GetQuoteDetails();
            responseMessage = Request.CreateResponse(HttpStatusCode.OK, quotes);

            return responseMessage;
        }

        [HttpPost]
        [Route("SearchPeople")]
        public HttpResponseMessage FindPeople(SearchFilter filter)
        {
            HttpResponseMessage responseMessage;

            if (filter == null)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect search filter parameters.");
                return responseMessage;
            }

            var searchResults = this._insuranceBusiness.FindPeople(filter);
            responseMessage = Request.CreateResponse(HttpStatusCode.OK, searchResults);

            return responseMessage;
        }

        [HttpGet]
        [Route("GetInsured")]
        public HttpResponseMessage GetAdditionalInsuredForQuote(int personId, int quoteId)
        {
            HttpResponseMessage responseMessage;

            if (quoteId == 0)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect parameters.");
                return responseMessage;
            }

            var additionalInsuredInfo = this._insuranceBusiness.GetAdditionalInsuredForQuote(personId, quoteId);
            responseMessage = Request.CreateResponse(HttpStatusCode.OK, additionalInsuredInfo);

            return responseMessage;
        }

        [HttpPost]
        [Route("AddInsured")]
        public HttpResponseMessage AddAdditionalInsured(AdditionalInsured insured)
        {
            HttpResponseMessage responseMessage;

            if (insured == null)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect parameters.");
                return responseMessage;

            }
            this._insuranceBusiness.AddAdditionalInsured(insured);
            responseMessage = Request.CreateResponse(HttpStatusCode.OK, "Added successfully.");

            return responseMessage;
        }

        [HttpGet]
        [Route("RemoveInsured")]
        public HttpResponseMessage RemoveAdditionalInsured(int insuredId)
        {
            HttpResponseMessage responseMessage;

            if (insuredId == 0)
            {
                responseMessage = Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect parameters.");
                return responseMessage;

            }
            this._insuranceBusiness.RemoveAdditionalInsured(insuredId);
            responseMessage = Request.CreateResponse(HttpStatusCode.OK, "Removed successfully.");

            return responseMessage;
        }
    }
}
