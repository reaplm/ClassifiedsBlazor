
using cb.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace cb.api.Repository.Impl
{
    public class AdvertRepo : IAdvertRepo
    {
        readonly
        private ApplicationContext _context;
        public AdvertRepo(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<List<Advert>> FindAll()
        {
            return await _context.Adverts
              .Include(x => x.Detail).ToListAsync();

                
        }

        public async Task<Advert> FindById(int id)
        {
             return await _context.Adverts
                 .Where(x => x.ID == id)
                 .Include(x => x.Detail)
                 .FirstOrDefaultAsync();


        }
    }
}
