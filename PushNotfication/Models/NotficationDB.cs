using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PushNotfication.Models
{
    public partial class NotficationDB: DbContext
    {
        public NotficationDB()
        {

        }

        public NotficationDB(DbContextOptions<NotficationDB> options)
            : base(options)
        {
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)=> options.UseSqlite("DataSource=NotificationDB.db");
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<NotificationHistory> NotificationHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);
                entity.Property<int>(e => e.Id).HasDefaultValue(DatabaseGeneratedOption.Identity);
            });

            modelBuilder.Entity<NotificationHistory>(entity =>
            {
                entity.ToTable("NotificationHistories");
                entity.HasKey(e => e.Id);
                entity.Property<int>(e => e.Id).HasDefaultValue(DatabaseGeneratedOption.Identity);
                entity.HasOne(e => e.User).WithMany(u=> u.NotificationHistories)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
