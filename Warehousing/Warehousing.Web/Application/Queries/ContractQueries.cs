using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Warehousing.Web.Models.DataTables;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.Queries
{
    public class ContractQueries
    {
        private string _connectionString;
        private const string BASE_QUERY = @"
SELECT    
    C.[Id],
    C.[Description],
    C.[StartDate],
    C.[PaymentDueUntil],
    C.[IsPayed],
    C.[AddDate],
    C.[SupplierId],
    S.[Name] AS [SupplierName]
FROM [Contract] C
LEFT JOIN [Supplier] S ON C.[SupplierId] = S.[Id]
";

        public ContractQueries(string connString)
        {
            _connectionString = connString;
        }

        public IEnumerable<ContractDTO> GetContractsSimple()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = BASE_QUERY;

                var res = conn.Query<ContractDTO>(baseQuery);

                conn.Close();

                return res;
            }
        }

        public DTResponse GetContracts(DTParameterModel dtrequest)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = BASE_QUERY;

                var searchColumns = new List<string>()
                {
                    "Description", "SupplierName"
                };

                var query = DTHelper.ProcessRequestSQL(baseQuery, dtrequest, searchColumns, false);
                var res = conn.Query<ContractDTO>(query);

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
