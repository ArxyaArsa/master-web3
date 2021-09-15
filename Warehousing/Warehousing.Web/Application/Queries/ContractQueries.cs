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

        public ContractQueries(string connString)
        {
            _connectionString = connString;
        }

        public IEnumerable<ContractDTO> GetContractsSimple()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = $@"
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

                
                var res = conn.Query<ContractDTO>(baseQuery);

                conn.Close();

                return res;
            }
        }
    }
}
