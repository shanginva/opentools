using Microsoft.EntityFrameworkCore;

namespace OpenTools.Repository.EntityFramework.Tests;

internal class TestDataContext : DbContext
{
    public TestDataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TestDataContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TestEntity> TestEntities { get; internal set; }
}
