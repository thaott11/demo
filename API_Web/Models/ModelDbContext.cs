using Microsoft.EntityFrameworkCore;

namespace API_Web.Models
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext()
        {
            
        }
        public ModelDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=demo22;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }

        public DbSet<Products> products { get; set; }
    }
}
