using cb.api.Entities;

namespace cb.api.Repository
{
    public interface IAdvertRepo
    {
        Task<List<Advert>> FindAll();
        Task<Advert> FindById(int id);
    }
}
