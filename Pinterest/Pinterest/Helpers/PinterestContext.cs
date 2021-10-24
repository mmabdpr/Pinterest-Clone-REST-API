using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pinterest.Models;

namespace Pinterest.Helpers
{
    public class PinterestContext : DbContext
    {
        public PinterestContext(DbContextOptions<PinterestContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=Aa12345;Database=pinterest_db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Board

            modelBuilder.Entity<Board>()
                .HasIndex(b => b.Name)
                .IsUnique();

            #endregion

            #region User

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            #endregion

            #region Followings

            modelBuilder.Entity<Following>()
                .HasKey(f => new {f.FolloweeId, f.FollowerId});

            modelBuilder.Entity<Following>()
                .HasOne(f => f.Followee)
                .WithMany(f => f.Followings)
                .HasForeignKey(f => f.FolloweeId);
            
            modelBuilder.Entity<Following>()
                .HasOne(f => f.Follower)
                .WithMany(f => f.Followings)
                .HasForeignKey(f => f.FollowerId);

            #endregion
        }
        
        public DbSet<Board> Boards { get; set; }
        public DbSet<Pin> Pins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Following> Followings { get; set; }
    }
}