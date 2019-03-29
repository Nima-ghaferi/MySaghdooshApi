using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entities
{
    public class ServerPhoto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public int ServerId { get; set; }
        public Server Server { get; set; }
        public bool IsPrimary { get; set; }
    }

    public class ServerPhotoConfig : EntityTypeConfiguration<ServerPhoto>
    {
        public ServerPhotoConfig()
        {
            ToTable("MS_ServerPhotos");
            HasKey<int>(s => s.Id);
            this.Property(p => p.ServerId)
                .IsRequired();
            this.Property(p => p.Photo)
                .HasColumnType("image")
                .IsRequired();
            this.Property(p => p.ServerId)
                .IsRequired();
            this.Property(p => p.IsPrimary)
                .IsRequired();
        }
    }
}
