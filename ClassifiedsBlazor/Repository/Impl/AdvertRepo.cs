using ClassifiedsBlazor.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsBlazor.Repository.Impl
{
    public class AdvertRepo : IAdvertRepo
    {
        readonly
        private ApplicationContext _context;
        public AdvertRepo(ApplicationContext context)
        {
            _context = context;
        }
        public Task<List<Advert>> FindAll()
        {
            return _context.Adverts
                .Include(x => x.Detail).ToListAsync();
                
        }
    }
}
