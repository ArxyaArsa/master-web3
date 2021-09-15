using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Warehousing.Web.Models.DataTables;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.Queries
{
    public class ParcelQueries
    {
        private string _connectionString;

        public ParcelQueries(string connString)
        {
            _connectionString = connString;
        }

        public DTResponse GetParcels(int whId, DTParameterModel dtrequest)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = $@"
SELECT    
    P.[Id],
    CONCAT('P', P.[Id]) AS [Name],
    P.[ContractId],
    C.[Description] AS [ContractName],
    P.[ParcelTypeId],
    PT.[Name] AS [ParcelTypeName],
    P.[WarehouseLotId],
    P.[Weight],
    P.[AddDate],
    P.[ValidUntil],
    P.[IsRemoved],
    P.[RemovedDate]
FROM [Parcel] P
LEFT JOIN [ParcelType] PT ON P.[ParcelTypeId] = PT.[Id]
LEFT JOIN [Contract] C ON P.[ContractId] = C.[Id]
WHERE WarehouseLotId = {whId} AND [IsRemoved] != 1
";

                var searchColumns = new List<string>()
                {
                    "Name", "ContractName", "ParcelTypeName"
                };

                var query = DTHelper.ProcessRequestSQL(baseQuery, dtrequest, searchColumns, false);
                var res = conn.Query<ParcelDTO>(query);

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
