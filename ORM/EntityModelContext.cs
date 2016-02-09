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
        public virtual DbSet<Lot> Lots { get; set; }
        //public virtual DbSet<LotRequest> LotRequests { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<LotImage> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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

            //modelBuilder.Entity<LotRequest>()
            //      .HasRequired<Lot>(r => r.Lot)
            //      .WithMany(l => l.LotRequests)
            //      .HasForeignKey(r => r.LotRefId)
            //      .WillCascadeOnDelete(false); ;

            //modelBuilder.Entity<LotRequest>()
            //      .HasRequired<Category>(r => r.Category)
            //      .WithMany(c => c.LotRequests)
            //      .HasForeignKey(r => r.CategoryRefId)
            //      .WillCascadeOnDelete(false);

            modelBuilder.Entity<Bid>()
                  .HasRequired<User>(b => b.User)
                  .WithMany(u => u.Bids)
                  .HasForeignKey(b => b.UserRefId)
                  .WillCascadeOnDelete(false); ;

            modelBuilder.Entity<Bid>()
                  .HasRequired<Lot>(b => b.Lot)
                  .WithMany(l => l.Bids)
                  .HasForeignKey(b => b.LotRefId)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lot>()
                  .HasRequired<User>(l => l.Seller)
                  .WithMany(u => u.Lots)
                  .HasForeignKey(l => l.SellerRefId)
                  .WillCascadeOnDelete(false); ;

            modelBuilder.Entity<Lot>()
                  .HasRequired<Category>(l => l.Category)
                  .WithMany(c => c.Lots)
                  .HasForeignKey(l => l.CategoryRefId)
                  .WillCascadeOnDelete(false);

            modelBuilder.Entity<LotImage>()
                  .HasRequired<Lot>(i => i.Lot)
                  .WithMany(l => l.Images)
                  .HasForeignKey(i => i.LotRefId)
                  .WillCascadeOnDelete(false);
        }
    }
}
