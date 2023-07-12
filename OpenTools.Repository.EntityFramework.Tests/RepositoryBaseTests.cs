using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OpenTools.Repository.Abstractions;

namespace OpenTools.Repository.EntityFramework.Tests;

public class RepositoryBaseTests
{
    private readonly string dbName = $"TestDatabase_{Guid.NewGuid()}";
    private readonly List<TestEntity> entities = new()
        {
            new TestEntity(Guid.NewGuid(), "Name 1"),
            new TestEntity(Guid.NewGuid(), "Name 2"),
            new TestEntity(Guid.NewGuid(), "Name 3"),
            new TestEntity(Guid.NewGuid(), "Name 4"),
            new TestEntity(Guid.NewGuid(), "Name 5"),
        };

    [Fact]
    public async Task GetReturnsTheItemById()
    {
        Guid entityId = Guid.NewGuid();
        TestEntity entity = new(entityId, "Name");
        await using (TestDataContext context = CreateDataContext())
        {
            context.TestEntities.Add(entity);
            await context.SaveChangesAsync();
        }

        await using (TestDataContext context = CreateDataContext())
        {
            TestEntityRepository repository = new(context);
            TestEntity? result = await repository.Get(entityId);
            result.Should().BeEquivalentTo(entity);
        }
    }

    [Fact]
    public async Task GetReturnsNullIfTheEntityWithIdDoesNotExist()
    {
        Guid entityId = Guid.NewGuid();

        await using (TestDataContext context = CreateDataContext())
        {
            TestEntityRepository repository = new(context);
            TestEntity? result = await repository.Get(entityId);
            result.Should().BeNull();
        }
    }

    [Fact]
    public async Task GetWhereReturnsAllItems()
    {
        await using (TestDataContext context = CreateDataContext())
        {
            context.TestEntities.AddRange(entities);
            await context.SaveChangesAsync();
        }

        await using (TestDataContext context = CreateDataContext())
        {
            TestEntityRepository repository = new(context);
            List<TestEntity> result = await repository.GetWhere(new All<TestEntity>());
            result.Should().HaveCount(entities.Count);
        }
    }

    [Fact]
    public async Task GetPagedWhereReturnsPagedResult()
    {
        await using (TestDataContext context = CreateDataContext())
        {
            context.TestEntities.AddRange(entities);
            await context.SaveChangesAsync();
        }

        await using (TestDataContext context = CreateDataContext())
        {
            TestEntityRepository repository = new(context);
            PagedResult<TestEntity> result = await repository.GetPagedWhere(new All<TestEntity>(), new PageInfo(2, 3));
            result.Items.Should().HaveCount(2);
            result.Items.Should().BeEquivalentTo(entities.Skip(3));
            result.Page.Should().Be(2);
            result.PagesCount.Should().Be(2);
            
            result = await repository.GetPagedWhere(new All<TestEntity>(), new PageInfo(1, 2));
            result.Items.Should().HaveCount(2);
            result.Items.Should().BeEquivalentTo(entities.Take(2));
            result.Page.Should().Be(1);
            result.PagesCount.Should().Be(3);
        }
    }

    private TestDataContext CreateDataContext() => new TestDataContext(
        new DbContextOptionsBuilder<TestDataContext>()
            .UseInMemoryDatabase(dbName)
            .Options);
}