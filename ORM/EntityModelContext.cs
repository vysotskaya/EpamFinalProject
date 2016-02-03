using System.Data.Entity;

namespace ORM
{
    public class EntityModelContext : DbContext
    {
        public EntityModelContext() 
            : base("name=EntityModelContext")
        {
            Database.SetInitializer<EntityModelContext>(new OnlineAuctionDBInitializer());
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Role>()
            //    .HasMany(e => e.Users)
            //    .WithRequired(e => e.Role)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany<Role>(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.ToTable("UserRole");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });

            modelBuilder.Entity<Category>()
                  .HasRequired<Section>(c => c.Section)
                  .WithMany(s => s.Categories)
                  .HasForeignKey(c => c.SectionRefId);

            modelBuilder.Entity<Section>()
                  .HasRequired<User>(s => s.User)
                  .WithMany(u => u.Sections)
                  .HasForeignKey(s => s.UserRefId)
                  .WillCascadeOnDelete(false); 

            modelBuilder.Entity<Request>()
                  .HasRequired<Category>(r => r.Category)
                  .WithMany(c => c.Requests)
                  .HasForeignKey(r => r.CategoryRefId)
                  .WillCascadeOnDelete(false); ;

            modelBuilder.Entity<Request>()
                  .HasRequired<Section>(r => r.Section)
                  .WithMany(s => s.Requests)
                  .HasForeignKey(r => r.SectionRefId)
                  .WillCascadeOnDelete(false);
        }
    }
}
