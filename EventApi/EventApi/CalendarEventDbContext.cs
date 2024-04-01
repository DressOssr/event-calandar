using EventApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EventApi
{
    public partial class CalendarEventDbContext : DbContext
    {
        public CalendarEventDbContext()
        {
        }
        public CalendarEventDbContext(DbContextOptions<CalendarEventDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Token>(entity =>
            {

                entity.Property(e => e.ExpiryDate).HasColumnType("smalldatetime");
                entity.Property(e => e.TokenHash)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(e => e.TokenSalt)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(e => e.Timestamp)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("TS");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Token_User");
                entity.ToTable("Token");
            });
            modelBuilder.Entity<Event>(entity =>
            {

                entity.Property(e => e.EventName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Timestamp)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("TS");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Event_User");
                entity.ToTable("Event");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Timestamp)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("TS");
                entity.ToTable("User");
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
