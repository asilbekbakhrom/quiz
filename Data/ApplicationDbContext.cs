using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using quizz.Entities;
using quizz.Utils;

namespace quizz.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Topic>? Topics { get; set; }
    public DbSet<Quizz>? Quizzs { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        AddNameHash();
        AddPassWordHash();

        SetDates();

        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddNameHash();
        AddPassWordHash();

        SetDates();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetDates()
    {
        foreach(var entry in ChangeTracker.Entries<Topic>())
        {
            if(entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }

            if(entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }
        foreach(var entry in ChangeTracker.Entries<Quizz>())
        {
            if(entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }

            if(entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }
    }

    private void AddNameHash()
    {
        foreach(var entry in ChangeTracker.Entries<Topic>())
        {
            if(entry.Entity is Topic topic)
                topic.NameHash = topic?.Name?.ToLower().Sha256();
        }
    }

     private void AddPassWordHash()
    {
        foreach(var entry in ChangeTracker.Entries<Quizz>())
        {
            if(entry.Entity is Quizz topic)
                topic.PassWordHash = topic?.PassWord?.ToLower().Sha256();
        }
    }
}