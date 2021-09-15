using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Warehousing.Web.Models.DataTables;
using Warehousing.Web.Models.DTOs;

namespace Warehousing.Web.Application.Queries
{
    public class SupplierQueries
    {
        private string _connectionString;

        public SupplierQueries(string connString)
        {
            _connectionString = connString;
        }

        const string BASE_QUERY = @"
SELECT    
    S.[Id],
    S.[Name],
    S.[Description],
    S.[Contact_FirstName],
    S.[Contact_LastName],
    S.[Contact_Phone],
    S.[Contact_Email],
    S.[Contact_Fax],
    S.[Address_State],
    S.[Address_Country],
    S.[Address_PostalCode],
    S.[Address_AddressLine1],
    S.[Address_AddressLine2],
    S.[AddDate],
    S.[FirstContractDate],
    S.[LastContractDate]
FROM [Supplier] S
";

        public IEnumerable<SupplierDTO> GetSuppliersSimple()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = BASE_QUERY;

                var res = conn.Query<SupplierDTO>(baseQuery);

                conn.Close();

                return res;
            }
        }

        public DTResponse GetSuppliers(DTParameterModel dtrequest)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var baseQuery = BASE_QUERY;

                var searchColumns = new List<string>()
                {
                    "Name", "Description", "Contact_FirstName", "Contact_LastName", "Address_State", "Address_Country", "Contact_Email", "Address_PostalCode"
                };

                var query = DTHelper.ProcessRequestSQL(baseQuery, dtrequest, searchColumns, false);
                var res = conn.Query<SupplierDTO>(query);

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
