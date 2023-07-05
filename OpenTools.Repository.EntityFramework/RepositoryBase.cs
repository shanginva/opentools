using Microsoft.EntityFrameworkCore;
using OpenTools.Repository.Abstractions;
using OpenTools.Repository.Abstrations;

namespace OpenTools.Repository.EntityFramework;

public abstract class RepositoryBase<TParam, TId>
    : IRepository<TParam, TId> 
        where TParam : EntityBase<TId>
        where TId: IEquatable<TId>
{
    private readonly DbContext context;

    protected RepositoryBase(DbContext context)
        => this.context = context;

    public virtual void Create(TParam param)
        => context.Add(param);

    public void Delete(TParam param)
        => context.Remove(param);

    public virtual void Update(TParam param)
        => context.Update(param);

    public virtual Task<TParam?> Get(
        TId id, 
        CancellationToken cancellationToken = default)
        => Query()
            .FirstOrDefaultAsync(param => param.Id.Equals(id));

    public virtual Task<List<TParam>> GetWhere(
        SpecificationBase<TParam> specification,
        CancellationToken cancellationToken = default)
        => Query()
            .Where(specification.Expression)
            .ToListAsync(cancellationToken);

    protected abstract IQueryable<TParam> Query();

}
