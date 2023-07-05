namespace OpenTools.Repository.Abstractions;
public abstract class EntityBase<TId>
{
    protected EntityBase(TId id)
    {
        Id = id;
    }

    public TId Id { get; }
}
