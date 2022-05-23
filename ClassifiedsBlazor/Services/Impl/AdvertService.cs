using ClassifiedsBlazor.Entities;
using ClassifiedsBlazor.Repository;

namespace ClassifiedsBlazor.Services.Impl
{
    public class AdvertService : IAdvertService
    {
        private IAdvertRepo _advertRepo;
        public AdvertService(IAdvertRepo advertRepo)
        {
            _advertRepo = advertRepo;
        }
        public Task<List<Advert>> FindAll()
        {
            return _advertRepo.FindAll();
        }
    }
}
