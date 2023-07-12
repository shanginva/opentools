namespace OpenTools.Repository.EntityFramework.Tests;

internal class TestEntityRepository 
    : RepositoryBase<TestDataContext, TestEntity, Guid>
{
    public TestEntityRepository(TestDataContext context) : base(context)
    {
    }

    protected override IQueryable<TestEntity> Query()
        => context.TestEntities.AsQueryable();
}
