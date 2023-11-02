using ContactManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Infrastructure.Context;

public class ContactManagerDbContext : DbContext
{
    public ContactManagerDbContext(DbContextOptions<ContactManagerDbContext> options) : base(options)
    {
        
    }

    public DbSet<Contact> Contacts { get; set; } = default!;
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.Property(c => c.Salutation)
                .IsRequired()
                .HasMaxLength(50); 

            entity.Property(c => c.Firstname)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(c => c.Lastname)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(255);
            entity.HasIndex(c => c.Email)
                .IsUnique();

            // entity.Property(c => c.CreationTimestamp)
            //     .IsRequired()
            //     .ValueGeneratedOnAdd()
            //     .HasDefaultValueSql("now()");
            //
            // entity.Property(c => c.LastChangeTimestamp)
            //     .IsRequired()
            //     .ValueGeneratedOnAddOrUpdate()
            //     .HasDefaultValueSql("now()");
        });
    }
}
