using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entities
{
    public class Token
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string TokenStr { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUsageTime { get; set; }
    }

    public class TokenConfig : EntityTypeConfiguration<Token>
    {
        public TokenConfig()
        {
            ToTable("MS_Tokens");
            HasKey<int>(s => s.Id);
            this.Property(p => p.TokenStr)
                .IsRequired()
                .HasMaxLength(500);
            this.Property(p => p.IsActive)
                .IsRequired();
            this.Property(p => p.LastUsageTime)
                .IsRequired();
        }
    }


}
