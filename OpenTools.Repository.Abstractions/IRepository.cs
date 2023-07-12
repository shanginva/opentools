namespace OpenTools.Repository.Abstractions;

public interface IRepository<TParam, TId>
{
    Task<TParam?> Get(
        TId id,
        CancellationToken cancellationToken = default);

    Task<List<TParam>> GetWhere(
        SpecificationBase<TParam> specification, 
        CancellationToken cancellationToken = default);

    Task<PagedResult<TParam>> GetPagedWhere( 
        SpecificationBase<TParam> specification, 
        PageInfo pageInfo,
        CancellationToken cancellationToken = default);

    void Create(TParam param);

    void Update(TParam param);

    void Delete(TParam param);
}
