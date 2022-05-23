using ClassifiedsBlazor.Entities;

namespace ClassifiedsBlazor.Services
{
    public interface IAdvertService
    {
        Task<IEnumerable<Advert>> FindAll();
    }
}
