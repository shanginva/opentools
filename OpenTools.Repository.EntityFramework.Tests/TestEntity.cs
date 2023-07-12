using OpenTools.Repository.Abstractions;

namespace OpenTools.Repository.EntityFramework.Tests;

public class TestEntity : EntityBase<Guid>
{
    public TestEntity(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }
}
