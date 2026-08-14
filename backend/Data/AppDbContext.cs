using CampusLostFound.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusLostFound.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<LostFoundItem> LostFoundItems => Set<LostFoundItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var item = modelBuilder.Entity<LostFoundItem>();

        item.Property(i => i.Type)
            .HasConversion<string>()
            .HasMaxLength(10);

        item.Property(i => i.Status)
            .HasConversion<string>()
            .HasMaxLength(10);

        item.HasData(
            new LostFoundItem
            {
                Id = 1,
                Title = "Black Wallet",
                Description = "Small black leather wallet with a few cards inside.",
                Category = "Accessories",
                Location = "Library",
                DateReported = new DateOnly(2026, 8, 10),
                Type = ItemType.LOST,
                Status = ItemStatus.ACTIVE,
                ContactName = "Alex Chen",
                ContactEmail = "alex.chen@example.edu"
            },
            new LostFoundItem
            {
                Id = 2,
                Title = "Blue Water Bottle",
                Description = "Blue metal water bottle with a dented cap.",
                Category = "Personal Items",
                Location = "Cafeteria",
                DateReported = new DateOnly(2026, 8, 11),
                Type = ItemType.FOUND,
                Status = ItemStatus.ACTIVE,
                ContactName = "Priya Nair",
                ContactEmail = "priya.nair@example.edu"
            },
            new LostFoundItem
            {
                Id = 3,
                Title = "Student ID Card",
                Description = "University ID card, name partially visible.",
                Category = "Documents",
                Location = "Computer Lab",
                DateReported = new DateOnly(2026, 8, 12),
                Type = ItemType.LOST,
                Status = ItemStatus.ACTIVE,
                ContactName = "Jordan Lee",
                ContactEmail = "jordan.lee@example.edu"
            },
            new LostFoundItem
            {
                Id = 4,
                Title = "Black Backpack",
                Description = "Black backpack with a laptop sleeve and a keychain.",
                Category = "Bags",
                Location = "Main Auditorium",
                DateReported = new DateOnly(2026, 8, 12),
                Type = ItemType.FOUND,
                Status = ItemStatus.ACTIVE,
                ContactName = "Sam Ortiz",
                ContactEmail = "sam.ortiz@example.edu"
            },
            new LostFoundItem
            {
                Id = 5,
                Title = "Wireless Earbuds",
                Description = "White wireless earbuds in a charging case.",
                Category = "Electronics",
                Location = "Lecture Hall",
                DateReported = new DateOnly(2026, 8, 13),
                Type = ItemType.LOST,
                Status = ItemStatus.ACTIVE,
                ContactName = "Maria Gomez",
                ContactEmail = "maria.gomez@example.edu"
            }
        );
    }
}
