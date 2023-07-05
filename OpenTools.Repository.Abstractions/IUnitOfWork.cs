namespace OpenTools.Repository.Abstractions;

public interface IUnitOfWork
{
    Task SaveChanges(CancellationToken cancellationToken = default);
}
