using ClassifiedsBlazor.Entities;
using Newtonsoft.Json;

namespace ClassifiedsBlazor.Services.Impl
{
    public class AdvertService : IAdvertService
    {
        readonly
        private HttpClient _httpClient;
        public AdvertService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Advert>> FindAll()
        {
            var adverts = new List<Advert>();

            try
            {
                var response = await _httpClient.GetAsync("api/Advert");
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
