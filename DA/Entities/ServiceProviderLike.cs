using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entities
{
    public class ServiceProviderLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ServerId { get; set; }
        public Server Server { get; set; }
    }

    public class ServiceProviderLikeConfig : EntityTypeConfiguration<ServiceProviderLike>
    {
        public ServiceProviderLikeConfig()
        {
            ToTable("MS_ServiceProviderLikes");
            HasKey<int>(s => s.Id);
            this.Property(p => p.UserId)
                .IsRequired();
            this.Property(p => p.ServerId)
                .IsRequired();
        }
    }
}
