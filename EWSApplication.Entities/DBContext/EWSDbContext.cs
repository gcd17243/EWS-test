namespace EWSApplication.Entities.DBContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EWSDbContext : DbContext
    {
        public EWSDbContext()
            : base("name=EWSDbContext")
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Staff> Students { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EWSDbContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAccount>()
                .Property(e => e.username)
                .IsFixedLength();
        }
    }
}
