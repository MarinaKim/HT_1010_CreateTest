namespace CreateTest.Lib.model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelEntity : DbContext
    {
        public ModelEntity()
            : base("name=ModelEntity")
        {
        }

        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Sections> Sections { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Questions>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Questions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sections>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.Sections)
                .WillCascadeOnDelete(false);
        }
    }
}
