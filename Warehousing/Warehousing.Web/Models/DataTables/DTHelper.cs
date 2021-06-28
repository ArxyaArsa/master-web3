using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warehousing.Web.Models.DataTables
{
    public class DTHelper
    {
        public static string ProcessRequestSQL(string baseQuery, DTParameterModel request, List<string> columnsToSearch, bool countQuery, bool noFilter = false)
        {
            var query = $@"
SELECT {Selecting(countQuery)}
FROM (
    {baseQuery}
) TMP
{(
((countQuery && noFilter) || ((request.Search?.Value ?? "") != "" && (columnsToSearch != null && columnsToSearch.Count > 0))) ? "" : 
$"WHERE {columnsToSearch.Select(x => x + $" LIKE '%{request.Search.Value}%'").Aggregate((c, n) => c + " OR " + n)}"
)}
{(countQuery ? "/*" : "")}
ORDER BY {request.Order.Select(x => request.Columns.ElementAt(x.Column).Data + " " + x.Dir).Aggregate((c, n) => c + ", " + n)}
OFFSET {request.Start} ROWS
FETCH NEXT {request.Length} ROWS ONLY
{(countQuery ? "*/" : "")}
";
            return query;
        }

        private static string Selecting(bool count)
        {
            return count ? "COUNT(*)" : "*";
        }
    }
}
