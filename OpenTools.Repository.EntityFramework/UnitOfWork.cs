using Microsoft.EntityFrameworkCore;
using OpenTools.Repository.Abstractions;

namespace OpenTools.Repository.EntityFramework;

public class UnitOfWork: IUnitOfWork
{
    private DbContext context;

    public UnitOfWork(DbContext context) 
        => this.context = context;

    public async Task SaveChanges(CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);
}
