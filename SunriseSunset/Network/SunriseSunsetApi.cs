using RestSharp;
using SunriseSunset.Extensions;
using SunriseSunset.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SunriseSunset.Network
{
    public class SunriseSunsetApi : ISunriseSunsetApi
    {
        private readonly string _url;

        private IRestClient _restClient;
        private IRestClient RestClient => _restClient = _restClient ?? new RestClient(_url) { Timeout = 5000 };

        public SunriseSunsetApi(string url) => _url = url;
        
        public async Task<SunriseSunsetModel> GetSunriseSunsetMessageAsync(double latitude, double longitude)
        {
            var request = new RestRequest(Method.GET);

            request.AddParameter("lat", latitude);
            request.AddParameter("lng", longitude);

            var restResponse = await ExecuteRequestAsync(request);

            var jsonSettings = new JsonSerializerSettings
            {
                DateFormatString = "h:mm:ss tt", DateTimeZoneHandling = DateTimeZoneHandling.Utc
            };
            var sunriseSunsetResponse = DeserializeResponse<SunriseSunsetApiResponseModel>(restResponse, jsonSettings);

            return sunriseSunsetResponse.SunriseSunset;
        }


        private async Task<IRestResponse> ExecuteRequestAsync(IRestRequest request)
        {
            var restResponse = await RestClient.ExecuteAsync(request);

            if (restResponse.ResponseStatus == ResponseStatus.Error)
            {
                throw new ApplicationException("Network transport error (no internet connection, failed DNS lookup, etc).");
            }

            return restResponse;
        }

        private T DeserializeResponse<T>(IRestResponse response, JsonSerializerSettings settings) where T : ApiRequestError
        {
            var result = response.Content.To<T>(settings);
            if (result.Status != "OK")
            {
                throw new Exception($"Request error status {result.Status}");
            }
            return result;
        }

    }
}