using Dapper;
using System.Collections.Generic;

namespace SigmaHubApi.Interface
{
    public interface IDataAccessManager
    {
        public List<T> ExecuteStoredProcedure<T>(string query, DynamicParameters parameters);
    }
}
