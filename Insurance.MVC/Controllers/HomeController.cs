using System.Collections.Generic;
using System.Web.Mvc;
using Insurance.Common.DTO;
using Insurance.MVC.Providers;

namespace Insurance.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly MasterModel masterModel;

        public HomeController()
        {
            masterModel = masterModel ?? new MasterModel() { 
                Quote = new Quote(),
                SearchResult = new List<SearchResult>(),
                AdditionalInsureds = new List<AdditionalInsured>()
            };
        }

        /// <summary>
        /// Index page that loads quote details by default.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            masterModel.Quote = ApiAccessProvider.GetData<Quote>(UrlProvider.QuoteUrl);

            return View(masterModel);
        }

        /// <summary>
        /// Search people
        /// </summary>
        /// <param name="searchFilter">Search filter</param>
        /// <returns>List of search results.</returns>
        public ActionResult SearchPeople(SearchFilter searchFilter)
        {
            searchFilter.FirstName = searchFilter.FirstName ?? string.Empty;
            searchFilter.LastName = searchFilter.LastName ?? string.Empty;

            masterModel.SearchResult = ApiAccessProvider.PostData<SearchFilter, List<SearchResult>>(searchFilter, UrlProvider.SearchPeopleUrl);

            return PartialView("SearchPeopleView", masterModel);
        }

        /// <summary>
        /// Add people to the quote
        /// </summary>
        /// <param name="additionalInsured">AdditionalInsured</param>
        /// <returns>List of additional insured.</returns>
        public ActionResult AddPeople(AdditionalInsured additionalInsured)
        {
            ApiAccessProvider.PostData<AdditionalInsured, string>(additionalInsured, UrlProvider.AddInsuredUrl);

            var url = UrlProvider.GetInsuredUrl + "?personid=" + additionalInsured.PersonId + "&quoteid=" + additionalInsured.QuoteId;
            masterModel.AdditionalInsureds = ApiAccessProvider.GetData<List<AdditionalInsured>>(url);
            return PartialView("AdditionalInsuredView", masterModel);
        }

        /// <summary>
        /// Remove people from the quote
        /// </summary>
        /// <param name="additionalInsured"></param>
        /// <returns></returns>
        public ActionResult RemovePeople(AdditionalInsured additionalInsured)
        {
            var url = UrlProvider.RemoveInsuredUrl + "?insuredId=" + additionalInsured.AdditionalInsuredId;
            ApiAccessProvider.GetData<string>(url);

            var urlGet = UrlProvider.GetInsuredUrl + "?personid=" + additionalInsured.PersonId + "&quoteid=" + additionalInsured.QuoteId;
            masterModel.AdditionalInsureds = ApiAccessProvider.GetData<List<AdditionalInsured>>(urlGet);
            return PartialView("AdditionalInsuredView", masterModel);
        }
    }
}