using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using SigmaHubApi.Interface;
using SigmaHubApi.Modal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SigmaHubApi.Repository
{
    public class DataAccessManager : IDataAccessManager
    {
        private readonly IConfiguration _config;
        private string DatabaseConnection = "DB";
        public DataAccessManager(IConfiguration config)
        {
            _config = config;
        }
        public List<T> ExecuteStoredProcedure<T>(string query, DynamicParameters parameters)
        {
            List<T> results = null;
            using (IDbConnection dbConnection = new OracleConnection(_config.GetConnectionString(DatabaseConnection)))
            {
                if (dbConnection.State == ConnectionState.Closed) { dbConnection.Open(); }
                using var transaction = dbConnection.BeginTransaction();
                try
                {
                    results = dbConnection.Query<T>(query, parameters, commandType: CommandType.StoredProcedure, transaction: transaction).ToList();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Logs.Writelog("ExecuteStoredProcedure" + ex.ToString());
                    throw;
                }
            };

            return results;
        }
    }
}
