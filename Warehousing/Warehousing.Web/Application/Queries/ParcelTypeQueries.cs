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

        public const string BASE_QUERY = @"
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

        public IEnumerable<ParcelTypeDTO> GetParcelTypesSimple()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = BASE_QUERY;
                
                var res = conn.Query<ParcelTypeDTO>(baseQuery);

                conn.Close();

                return res;
            }
        }

        public DTResponse GetParcelTypes(DTParameterModel dtrequest)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = BASE_QUERY;

                var searchColumns = new List<string>()
                {
                    "Name", "Description"
                };

                var query = DTHelper.ProcessRequestSQL(baseQuery, dtrequest, searchColumns, false);
                var res = conn.Query<ParcelTypeDTO>(query);

                query = DTHelper.ProcessRequestSQL(baseQuery, dtrequest, searchColumns, true, false);
                var countFiltered = conn.QuerySingle<int>(query);

                query = DTHelper.ProcessRequestSQL(baseQuery, dtrequest, searchColumns, true, true);
                var countAll = conn.QuerySingle<int>(query);

                conn.Close();

                return new DTResponse()
                {
                    Data = res,
                    Draw = dtrequest.Draw,
                    Error = null,
                    RecordsTotal = countAll,
                    RecordsFiltered = countFiltered,
                    AdditionalParameters = new Dictionary<string, object>()
                };
            }
        }
    }
}
