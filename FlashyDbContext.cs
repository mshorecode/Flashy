using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Hosting;
using Flashy.Data;
using Flashy.Models;

public class FlashyDbContext : DbContext
{
    public DbSet<Flashcard> Flashcards { get; set; }
    public DbSet<Set> Sets { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }

    public FlashyDbContext(DbContextOptions<FlashyDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Flashcard>().HasOne(f => f.Set).WithMany(s => s.Flashcards).HasForeignKey(f => f.SetId);
        modelBuilder.Entity<Set>().HasData(SetData.Sets);
        modelBuilder.Entity<Tag>().HasData(TagData.Tags);
        modelBuilder.Entity<User>().HasData(UserData.Users);
    }
}