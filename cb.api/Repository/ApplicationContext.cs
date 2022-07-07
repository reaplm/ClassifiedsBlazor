
using cb.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace cb.api.Repository
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Advert> Adverts { set; get; }
        public virtual DbSet<AdvertDetail> AdvertDetails { set; get; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
