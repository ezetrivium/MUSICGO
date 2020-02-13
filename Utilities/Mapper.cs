using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Mapper
    {
        public static TDestination Map<TSource, TDestination>(TSource entity)
        {
            return AutoMapper.Mapper.Map<TSource, TDestination>(entity);
        }

        public static IList<TDestination> Map<TSource, TDestination>(IList<TSource> entities)
        {
            return AutoMapper.Mapper.Map<IList<TSource>, IList<TDestination>>(entities);
        }
    }
}