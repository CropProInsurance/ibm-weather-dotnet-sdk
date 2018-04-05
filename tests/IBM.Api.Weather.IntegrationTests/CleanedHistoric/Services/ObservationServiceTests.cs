using System;
using System.Net.Http;
using System.Threading.Tasks;
using IBM.Api.Weather.CleanedHistoric.Options;
using IBM.Api.Weather.CleanedHistoric.QueryObjects;
using IBM.Api.Weather.CleanedHistoric.Serivces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Shouldly;

namespace IBM.Api.Weather.IntegrationTests.CleanedHistoric.Services {

    [TestFixture]
    public class ObservationServiceTests
    {
        private IConfiguration _configuration;
        private HttpClient _client;
        private IOptions<CleanHistoricOptions> _options;
        private IObservationService _observationService;

        [OneTimeSetUp]
        public void Setup() {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.local.json", false);
            _configuration = configurationBuilder.Build();
            _options = Options.Create<CleanHistoricOptions>(_configuration.GetSection("CleanHistoricOptions").Get<CleanHistoricOptions>());
            _observationService = new ObservationService(_options);
        }

        [Test]
        public async Task integration_cleaned_history_get_by_zipcode() {
            var qo = new StandardWeatherQO {
                StartDate = DateTime.UtcNow.AddDays(-1),
                EndDate = DateTime.UtcNow,
                Zipcode = "50332"
            };

            var results = await _observationService.FindAsync(qo);
            results.ShouldNotBe(null);
            results.ShouldNotBe(string.Empty);
        }
    }
}
