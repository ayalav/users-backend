using Microsoft.EntityFrameworkCore;
using UsersApi.Data.Entites;

namespace UsersApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(x => x.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Phone).IsRequired();
        });
    }


}