using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using BE.Entities;

namespace DA.Entities
{
    public class Activation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ActivationCode { get; set; }
        public bool IsActive { get; set; }
    }

    public class ActivationConfig : EntityTypeConfiguration<Activation>
    {
        public ActivationConfig()
        {
            ToTable("MS_Activations");
            HasKey<int>(s => s.Id);
            this.Property(p => p.ActivationCode)
                .IsRequired()
                .HasMaxLength(10);
            this.Property(p => p.IsActive)
                .IsRequired();
        }
    }
}
