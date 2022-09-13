using ClassifiedsBlazor.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ClassifiedsBlazor.Services.Impl
{
    public class AdvertService : IAdvertService
    {
        readonly
        private HttpClient _httpClient;
        readonly
            private ILogger _logger;

        public AdvertService(HttpClient httpClient, ILogger<AdvertService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IEnumerable<Advert>> FindAll()
        {
            var adverts = new List<Advert>();

            try
            {
                var response = await _httpClient.GetAsync("api/Advert");

                _logger.LogInformation("_httpClient BaseAddress: " + _httpClient.BaseAddress +
                    "\nDefaultRequestHeaders: " + _httpClient.DefaultRequestHeaders);
                _logger.LogInformation("Response from AdvertService: " + response.ToString());

                if (response.RequestMessage != null)
                {
                    //_logger.LogInformation("Request URI: " + response.RequestMessage.RequestUri);
                    _logger.LogInformation("Request Message from AdvertService: " + response.RequestMessage);
                }

                response.EnsureSuccessStatusCode();

                Task<String> result = response.Content.ReadAsStringAsync();
                adverts = JsonConvert.DeserializeObject<List<Advert>>(result.Result);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
               
            }
            
            return adverts;

        }

        public async Task<Advert> FindById(int id)
        {
            var response = await _httpClient.GetAsync("api/Advert/"+id);
            response.EnsureSuccessStatusCode();

            Task<String> result = response.Content.ReadAsStringAsync();
            var advert = JsonConvert.DeserializeObject<Advert>(result.Result);

            return advert;
        }
    }
}
