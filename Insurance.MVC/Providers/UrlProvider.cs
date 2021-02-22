using System.Web.Configuration;
using Insurance.Common.Constants;

namespace Insurance.MVC.Providers
{
    public class UrlProvider
    {
        public static string QuoteUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings[URLConstants.QuoteUrl].ToString();
            }
        }

        public static string SearchPeopleUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings[URLConstants.SearchPeopleUrl].ToString();
            }
        }

        public static string GetInsuredUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings[URLConstants.GetInsuredUrl].ToString();
            }
        }

        public static string AddInsuredUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings[URLConstants.AddInsuredUrl].ToString();
            }
        }

        public static string RemoveInsuredUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings[URLConstants.RemoveInsuredUrl].ToString();
            }
        }
    }
}