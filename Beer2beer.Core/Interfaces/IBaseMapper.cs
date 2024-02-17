namespace Beer2beer.Core.Interfaces;

public interface IBaseMapper<TSource, TDestination>
{
    TDestination MapModel(TSource source);
    IEnumerable<TDestination> MapList(IEnumerable<TSource> source);
}


