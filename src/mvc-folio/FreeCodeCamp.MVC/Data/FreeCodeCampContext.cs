using FreeCodeCamp.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FreeCodeCamp.MVC.Data;

public class FreeCodeCampContext : DbContext
{
    public FreeCodeCampContext(DbContextOptions<FreeCodeCampContext> options) : base(options) { }

    public DbSet<Item> Items { get; set; }
    public DbSet<SerialNumber> SerialNumbers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ItemClient> ItemClients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>()
            .ToTable("Items")
            .HasData(
            new Item { Id = 5, Name = "Microphone", Price = 40, SerialNumberId = 10 }
            );

        modelBuilder.Entity<SerialNumber>()
            .ToTable("SerialNumbers")
            .HasData(
            new SerialNumber { Id = 10, Name = "MIC150", ItemId = 5 }
            );

        modelBuilder.Entity<Category>()
            .ToTable("Categories")
            .HasData(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Books" }
            );

        modelBuilder.Entity<Client>()
            .ToTable("Clients");

        modelBuilder.Entity<ItemClient>().HasKey(ic => new
        {
            ic.ItemId,
            ic.ClientId
        });
        modelBuilder.Entity<ItemClient>()
            .HasOne(i => i.Item)
            .WithMany(ic => ic.ItemClients)
            .HasForeignKey(ic => ic.ItemId);
        modelBuilder.Entity<ItemClient>()
            .HasOne(i => i.Client)
            .WithMany(ic => ic.ItemClients)
            .HasForeignKey(ic => ic.ClientId);

        base.OnModelCreating(modelBuilder);
    }
}
