namespace ContactListMvc.Models.Repository.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BeeperDbContext : DbContext
    {
        public BeeperDbContext()
            : base("name=SqlServerConnection")
        {
        }

        public virtual DbSet<attributes> attributes { get; set; }
        public virtual DbSet<categories> categories { get; set; }
        public virtual DbSet<cities> cities { get; set; }
        public virtual DbSet<personattributes> personattributes { get; set; }
        public virtual DbSet<persons> persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<attributes>()
                .HasMany(e => e.personattributes)
                .WithRequired(e => e.attributes)
                .HasForeignKey(e => e.idattribute);

            modelBuilder.Entity<categories>()
                .HasMany(e => e.persons)
                .WithOptional(e => e.categories)
                .HasForeignKey(e => e.idcategory);

            modelBuilder.Entity<cities>()
                .HasMany(e => e.persons)
                .WithOptional(e => e.cities)
                .HasForeignKey(e => e.idcity);

            modelBuilder.Entity<persons>()
                .HasMany(e => e.personattributes)
                .WithRequired(e => e.persons)
                .HasForeignKey(e => e.idperson);
        }
    }
}
