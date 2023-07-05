namespace OpenTools.Repository.Abstrations;

public interface IRepository<TParam, TId>
{
    Task<TParam?> Get(
        TId id,
        CancellationToken cancellationToken = default);

    Task<TParam> GetWhere(
        SpecificationBase<TParam> specification, 
        CancellationToken cancellationToken = default);

    void Create(TParam param);

    void Update(TParam param);

    void Delete(TParam param);
}
