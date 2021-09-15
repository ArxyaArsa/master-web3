using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Warehousing.Web.Models.DataTables;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.Queries
{
    public class ParcelTypeQueries
    {
        private string _connectionString;

        public ParcelTypeQueries(string connString)
        {
            _connectionString = connString;
        }

        public IEnumerable<ParcelTypeDTO> GetParcelTypesSimple()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = $@"
SELECT    
    PT.[Id],
    PT.[Name],
    PT.[Description],
    PT.[MinWeightOccupied],
    PT.[MaxWeight],
    PT.[IsPerishable],
    PT.[FreezerLifetime],
    PT.[DryLifetime],
    COUNT(P.Id) AS [ParcelCount]
FROM [ParcelType] PT
LEFT JOIN [Parcel] P ON PT.[Id] = P.[ParcelTypeId] AND P.[IsRemoved] != 1
GROUP BY
    PT.[Id],
    PT.[Name],
    PT.[Description],
    PT.[MinWeightOccupied],
    PT.[MaxWeight],
    PT.[IsPerishable],
    PT.[FreezerLifetime],
    PT.[DryLifetime]
";

                
                var res = conn.Query<ParcelTypeDTO>(baseQuery);

                conn.Close();

                return res;
            }
        }
    }
}
