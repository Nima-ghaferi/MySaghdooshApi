using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entities
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class ServerConfig : EntityTypeConfiguration<Server>
    {
        public ServerConfig()
        {
            ToTable("MS_Servers");
            HasKey<int>(s => s.Id);
            this.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(p => p.Tel)
                .IsOptional()
                .HasMaxLength(100);
            this.Property(p => p.Address)
                .IsOptional()
                .HasMaxLength(2000);
            this.Property(p => p.IsActive)
                .IsRequired();
        }
    }
}
