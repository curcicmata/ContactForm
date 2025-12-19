using ContactForm.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactForm.Infrastructure.DB
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<ContactModel> Contacts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ContactModel>(entity =>
            {
                entity.ToTable("Contacts");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.Phone).HasMaxLength(50);
                entity.Property(e => e.Website).HasMaxLength(255);
                entity.Property(e => e.Company).HasMaxLength(255);
                entity.Property(e => e.Address).HasMaxLength(500);
            });
        }
    }
}
