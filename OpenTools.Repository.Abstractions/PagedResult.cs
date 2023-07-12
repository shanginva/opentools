namespace OpenTools.Repository.Abstractions;

public record PagedResult<TParam>(int Page, int PagesCount, List<TParam> Items);
