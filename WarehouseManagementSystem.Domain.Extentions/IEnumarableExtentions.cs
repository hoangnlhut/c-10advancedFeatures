using NPOI.SS.Formula.Functions;

namespace WarehouseManagementSystem.Domain.Extentions
{
    public static class IEnumarableExtentions
    {
        public static IEnumerable<T?> Find<T>(this IEnumerable<T> source, Func<T, bool> isMatch)
        {
            foreach (var item in source)
            {
                if (isMatch(item))
                { 
                    yield return item; 
                }
            }
        }
    }
}