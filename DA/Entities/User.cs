using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Tel { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
    }

    public class UserConfig : EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            ToTable("MS_Users");
            HasKey<int>(s => s.Id);
            this.Property(p => p.Tel)
                .IsRequired()
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_Tel", 1) { IsUnique = true }));
            this.Property(p => p.IsActive)
                .IsRequired();
            this.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
