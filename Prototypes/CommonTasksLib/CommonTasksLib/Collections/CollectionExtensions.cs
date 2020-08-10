using System;
using System.Collections.Generic;
using System.Data.Common;

namespace CommonTasksLib.Collections
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> Select<T, TDbType>(this TDbType reader, Func<TDbType, T> selector)
            where TDbType : DbDataReader
        {
            while (reader.Read())
            {
                yield return selector(reader);
            }
        }
    }
}
