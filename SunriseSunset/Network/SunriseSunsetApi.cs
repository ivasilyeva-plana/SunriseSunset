using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using RestSharp;

namespace SunriseSunset.Network
{
    public class SunriseSunsetApi
    {
        private readonly string _url;

        private IRestClient _restClient;
        private IRestClient RestClient => _restClient = _restClient ?? new RestClient(_url) { Timeout = 5000 };

        public SunriseSunsetApi(string url)
        {
            _url = url;
        }

        public async Task GetSunriseSunsetMessage(double latitude, double longitude)
        {
            var request = new RestRequest(Method.GET);

            request.AddParameter("uuid", latitude);
            request.AddParameter("token", longitude);

            var restResponse = await ExecuteRequestAsync(request);

        }


        private async Task<IRestResponse> ExecuteRequestAsync(IRestRequest request)
        {
            var restResponse = await RestClient.ExecuteAsync(request);

            if (restResponse.ResponseStatus == ResponseStatus.Error)
            {
                throw new ApplicationException("Network transport error (no internet connection, failed DNS lookup, etc).");
            }

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new ApplicationException(restResponse.ErrorMessage);
            }

            return restResponse;
        }


    }
}