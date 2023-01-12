namespace OpenTools.Repository.Abstrations;

internal interface IRepository<TParam, TId>
{
    Task<TParam> Get(TId id);

    Task<TParam> GetWhere(SpecificationBase<TParam> specification);

    void Create(TParam param);

    void Update(TParam param);

    void Delete(TId id);
}
