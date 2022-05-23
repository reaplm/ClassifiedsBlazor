using ClassifiedsBlazor.Entities;

namespace ClassifiedsBlazor.Services
{
    public interface IAdvertService
    {
        Task<List<Advert>> FindAll();
    }
}
