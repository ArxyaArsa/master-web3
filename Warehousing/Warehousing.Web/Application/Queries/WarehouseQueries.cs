using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Warehousing.Web.Models.DataTables;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.Queries
{
    public class WarehouseQueries
    {
        private string _connectionString;

        public WarehouseQueries(string connString)
        {
            _connectionString = connString;
        }

        public DTResponse GetWarehouses(DTParameterModel dtrequest)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = $@"
SELECT    
    WL.[Id],
    WL.[Name],
    WL.[Description],
    WL.[Type],
    WL.[Occupated],
    WL.[WeightCapacity],
    WL.[LastInventoryChange],
    WL.[Manager_FirstName],
    WL.[Manager_LastName],
    WL.[Manager_Phone],
    WL.[Manager_Email],
    COUNT(P.Id) AS ParcelCount,
    SUM(ISNULL(P.Weight, 0)) AS ParcelWeight
FROM [WarehouseLot] WL
LEFT JOIN [Parcel] P ON WL.Id = P.WarehouseLotId
GROUP BY 
    WL.[Id],
    WL.[Name],
    WL.[Description],
    WL.[Type],
    WL.[Occupated],
    WL.[WeightCapacity],
    WL.[LastInventoryChange],
    WL.[Manager_FirstName],
    WL.[Manager_LastName],
    WL.[Manager_Phone],
    WL.[Manager_Email]
";

                var searchColumns = new List<string>()
                {
                    "Name", "Description", "Manager_FirstName", "Manager_LastName"
                };

                var query = DTHelper.ProcessRequestSQL(baseQuery, dtrequest, searchColumns, false);
                var res = conn.Query<WarehouseLotDTO>(query);

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
                    TotalRecords = countAll,
                    TotalRecordsFiltered = countFiltered,
                    AdditionalParameters = new Dictionary<string, object>()
                };
            }
        }
    }
}
