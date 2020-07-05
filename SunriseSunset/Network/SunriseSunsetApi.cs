using RestSharp;
using SunriseSunset.Extensions;
using SunriseSunset.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SunriseSunset.Network
{
    public class SunriseSunsetApi : ISunriseSunsetApi
    {
        private readonly string _url;

        private IRestClient _restClient;
        private IRestClient RestClient => _restClient = _restClient ?? new RestClient(_url) { Timeout = 5000 };

        public SunriseSunsetApi(string url)
        {
            _url = url;
        }

        public async Task<SunriseSunsetModel> GetSunriseSunsetMessage(double latitude, double longitude)
        {
            var request = new RestRequest(Method.GET);

            request.AddParameter("lat", latitude);
            request.AddParameter("lng", longitude);

            var restResponse = await ExecuteRequestAsync(request);
            var sunriseSunsetResponse = DeserializeResponse<SunriseSunsetApiResponseModel>(restResponse);

            return sunriseSunsetResponse.SunriseSunset;
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

        private T DeserializeResponse<T>(IRestResponse response) where T : ApiRequestError
        {
            var result = response.Content.To<T>();
            if (result.Status != "OK")
            {
                throw new ApplicationException($"Request error status {result.Status}");
            }
            return result;
        }

    }
}