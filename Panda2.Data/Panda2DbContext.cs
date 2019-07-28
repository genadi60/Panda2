using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Panda2.Models;

namespace Panda2.Data
{
    public class Panda2DbContext : IdentityDbContext<PandaUser, PandaUserRole, string>
    {
        public Panda2DbContext(DbContextOptions<Panda2DbContext> options) :base(options)
        {
            
        }
        
        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PandaUser>()
                .HasKey(user => user.Id);

            builder.Entity<PandaUser>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<PandaUser>()
                .HasMany(user => user.Packages)
                .WithOne(package => package.Recipient)
                .HasForeignKey(package => package.RecipientId);

            builder.Entity<PandaUser>()
                .HasMany(user => user.Receipts)
                .WithOne(receipt => receipt.Recipient)
                .HasForeignKey(receipt => receipt.RecipientId);

            builder.Entity<PandaUserRole>()
                .HasKey(role => role.Id);

            builder.Entity<Receipt>()
                .HasOne(receipt => receipt.Package)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Receipt>()
                .Property(r => r.Fee)
                .HasColumnType("DECIMAL(18, 2)");

            base.OnModelCreating(builder);

        } 
    }
}