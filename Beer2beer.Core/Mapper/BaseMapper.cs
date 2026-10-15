using Beer2beer.Core.Interfaces;
using AutoMapper;

namespace Beer2beer.Core.Mapper;

public class BaseMapper<TSource, TDestination> : IBaseMapper<TSource, TDestination>
{
    private readonly IMapper _mapper;

    public BaseMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination MapModel(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }

    public IEnumerable<TDestination> MapList(IEnumerable<TSource> source)
    {
        // Mapping directly to List<TDestination> avoids AutoMapper's interface resolution
        // overhead and deferred iterator wrapper allocations when mapping IEnumerable collections.
        return _mapper.Map<List<TDestination>>(source);
    }
}
