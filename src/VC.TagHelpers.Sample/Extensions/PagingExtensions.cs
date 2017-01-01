using System.Collections.Generic;
using System.Linq;

namespace VC.TagHelpers.Sample.Extensions
{
    public static class PagingExtensions
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> data, int page, int pageSize)
        {
            return data.Skip((page - 1)*pageSize).Take(pageSize);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> data, int page, int pageSize)
        {
            return data.Skip((page - 1)*pageSize).Take(pageSize);
        }
    }
}