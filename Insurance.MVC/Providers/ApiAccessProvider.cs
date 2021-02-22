using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace Insurance.MVC.Providers
{
    public class ApiAccessProvider
    {
        public static bool IsSuccess { get; private set; }

        public static HttpStatusCode StatusCode { get; private set; }

        static ApiAccessProvider()
        {
        }

        public static T GetData<T>(string uri)
            where T : class
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return response.Content.ReadAsAsync<T>().Result;
            }
        }

        public static R PostData<T, R>(T data, string uri)
            where T : class
            where R : class
        {
            using (var client = new HttpClient())
            {
                var response = client.PostAsJsonAsync(uri, data).Result;

                StatusCode = response.StatusCode;
                IsSuccess = response.IsSuccessStatusCode;

                return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<R>().Result : default;
            }
        }
    }
}