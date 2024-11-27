using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI_assign.Utils
{
    public class MergeUtils
    {
        public static List<T> MergeUnique<T>(
            List<T> sourceList,
            List<T> defaultList,
            Func<T, string> uniqueKeySelector
        )
        {
            var seenKeys = new HashSet<string>();
            var result = new List<T>();

            foreach (var item in sourceList.Concat(defaultList))
            {
                var key = uniqueKeySelector(item) ?? string.Empty;
                if (seenKeys.Add(key))
                {
                    result.Add(item);
                }
            }

            return result;
        }



    }
}
