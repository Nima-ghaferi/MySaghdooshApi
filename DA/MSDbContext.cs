using System.Data.Entity;

namespace DA
{
    public class MSDbContext : DbContext
    {
        public MSDbContext() : base("name=MSDbContext")
        {
        }
        public DbSet<Entities.Category> Categories { get; set; }
        public DbSet<Entities.Server> Servers { get; set; }
        public DbSet<Entities.ServerPhoto> ServerPhotos { get; set; }
        public DbSet<Entities.User> Users { get; set; }
        public DbSet<Entities.Activation> Activations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Entities.CategoryConfig());
            modelBuilder.Configurations.Add(new Entities.ServerConfig());
            modelBuilder.Configurations.Add(new Entities.ServerPhotoConfig());
            modelBuilder.Configurations.Add(new Entities.UserConfig());
            modelBuilder.Configurations.Add(new Entities.ActivationConfig());
        }
    }
}
