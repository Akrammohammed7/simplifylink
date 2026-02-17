using Microsoft.EntityFrameworkCore;
using SimplifyLink.Api.Models;

namespace SimplifyLink.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ShortLink> ShortLinks => Set<ShortLink>();
    public DbSet<ClickEvent> ClickEvents => Set<ClickEvent>();

    protected override void OnModelCreating(ModelBuilder modeBuilder)
    {
        modeBuilder.Entity<ShortLink>()
        .HasIndex(x => x.ShortCode)
        .IsUnique();
    }

}
