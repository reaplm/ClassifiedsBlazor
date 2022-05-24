using ClassifiedsBlazor.Entities;

namespace ClassifiedsBlazor.Repository
{
    public interface IAdvertRepo
    {
        Task<List<Advert>> FindAll();
        Task<Advert> FindById(int id);
    }
}
