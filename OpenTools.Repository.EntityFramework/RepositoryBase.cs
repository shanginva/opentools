using Microsoft.EntityFrameworkCore;
using OpenTools.Repository.Abstractions;

namespace OpenTools.Repository.EntityFramework;

public abstract class RepositoryBase<TDataContext, TParam, TId>
    : IRepository<TParam, TId> 
        where TParam : EntityBase<TId>
        where TId: IEquatable<TId>
        where TDataContext : DbContext
{
    protected readonly TDataContext context;

    protected RepositoryBase(TDataContext context)
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

    public async Task<PagedResult<TParam>> GetPagedWhere(SpecificationBase<TParam> specification, PageInfo pageInfo, CancellationToken cancellationToken = default)
    {
        int skip = (pageInfo.Page - 1) * pageInfo.PageSize;
        IQueryable<TParam> query = Query().Where(specification.Expression);

        List<TParam> items = await query
            .Skip(skip)
            .Take(pageInfo.PageSize)
            .ToListAsync();
        return new PagedResult<TParam>(
            Page: pageInfo.Page, 
            PagesCount: (await query.CountAsync() + pageInfo.PageSize - 1) / pageInfo.PageSize, 
            Items: items);
    }

    protected abstract IQueryable<TParam> Query();

}
