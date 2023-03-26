using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace ict_technical_WebAPI.Model.Interface
{
        public interface IRepository<TEntity> where TEntity : class
        {
        CommonResponse getResponseBySp(string SpName);
        CommonResponse getResponseBySpWithParam(string SpName, params object[] parameterValues);
        int ExecuteAsync<T>(T table, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        int ExecuteAsync<T>(T table, string TableName , object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        CommonResponse getDatasetResponseBySp(string SpName);
        CommonResponse getDatasetResponseBySp(string SpName, params object[] paramValues);
        CommonResponse GetDatatableBySQL(string SQL);
        CommonResponse GetDatatableBySQL(string SQL, params SqlParameter[] sqlParameters);
        TEntity GetSQL(string SQL);
        int ExecuteAsync(string sql, SqlParameter[] param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        string GetMaxIdGen(string coloumName, string rightStringFormat, string prefix, string tableName);
        string GetMaxInoviceNoByShop(string tableName, string shopID, string prefix);
        string GetMaxId(string coloumName, string rightStringLength, string initialValue, string tableName, string prefix);
        CommonResponse GetDatatableBySQLGEN<T>(string SQL) where T : class;
    }
}
