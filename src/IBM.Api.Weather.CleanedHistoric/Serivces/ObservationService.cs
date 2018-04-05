using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IBM.Api.Weather.CleanedHistoric.Options;
using IBM.Api.Weather.CleanedHistoric.QueryObjects;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using IBM.Api.Weather.CleanedHistoric.Extensions;

namespace IBM.Api.Weather.CleanedHistoric.Serivces {
    public interface IObservationService {
        Task<string> FindAsync(StandardWeatherQO qo);
    }

    public class ObservationService : IObservationService {
        private readonly CleanHistoricOptions _options;

        public ObservationService(IOptions<CleanHistoricOptions> options) {
            _options = options.Value;
        }

        public async Task<string> FindAsync(StandardWeatherQO qo) {
            try {
                using (var client = new HttpClient()) {
                    var uri = new UriBuilder("http://cleanedobservations.wsi.com/CleanedObs.svc/GetObs");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-us"));

                    var queryString = qo.ToDictionary();
                    queryString.Add("userKey", _options.ApiKey);
                    queryString.Add("version", "2");
                    uri.Query = queryString.ToQueryString();
                    
                    var result = await client.GetAsync(uri.ToString());
                    var resultContent = await result.Content.ReadAsStringAsync();
                    return resultContent;
                }
            } catch (Exception e) {
                var t = e.Message;
                return null;
            }
        }
    }
}
