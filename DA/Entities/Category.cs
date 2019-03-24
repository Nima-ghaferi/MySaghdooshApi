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
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public static explicit operator BE.Entities.Request.CategoryResp(Category category)
        {
            var beCategory = new BE.Entities.Request.CategoryResp(category.Id, category.Name);
            var tt = new List<Category>();
            return beCategory;
        }        
    }

    public class CategoryConfig : EntityTypeConfiguration<Category>
    {
        public CategoryConfig()
        {
            ToTable("MS_Categories");
            HasKey<int>(s => s.Id);
            this.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(
                IndexAnnotation.AnnotationName,
                new IndexAnnotation(
                    new IndexAttribute("IX_Name", 1) { IsUnique = true }));
            this.Property(p => p.IsActive)
                .IsRequired();
        }
    }
}
