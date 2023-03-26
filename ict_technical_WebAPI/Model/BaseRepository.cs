using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Data;
using System.Reflection;

using System.IO;
using System.Web;
using ict_technical_WebAPI.Model.Interface;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Data.SqlClient;
using ict_technical_WebAPI.Model.Utility;
using ict_technical_WebAPI.Model.Helpers;

namespace ict_technical_WebAPI.Model
{
    public class BaseRepository<TEntity> :  IRepository<TEntity>  where TEntity : class 
    {
        #region All Common Response Method
        private readonly IDbConnection connection;
        public BaseRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public IConfiguration Configuration { get; }
        public int ExecuteAsync<T>(T table, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            try
            {
                int? queryTimeoutInSeconds = 3600;
                InsertQuary insert = new InsertQuary(table.GetType().Name);
                foreach (PropertyInfo prop in table.GetType().GetProperties())
                {
                    var attributes = prop.GetCustomAttributes(typeof(NOMAP), true);
                    if (attributes.Length > 0)
                    {
                        continue;
                    }
                    insert.Add(prop.Name.ToString().Trim(), prop.GetValue(table, null));
                }
                string sql = insert.ToString();

                return SqlHelper.ExecuteNonQuery(ConStr,CommandType.Text, sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int ExecuteAsync<T>(T table,string TableName, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            try
            {
                int? queryTimeoutInSeconds = 3600;
                InsertQuary insert = new InsertQuary(TableName);
                foreach (PropertyInfo prop in table.GetType().GetProperties())
                {
                    var attributes = prop.GetCustomAttributes(typeof(NOMAP), true);
                    if (attributes.Length > 0)
                    {
                        continue;
                    }
                    if (prop.GetValue(table, null) ==null )
                    {
                        continue;
                    }
                    insert.Add(prop.Name.ToString().Trim(), prop.GetValue(table, null));
                }
                string sql = insert.ToString();

                return SqlHelper.ExecuteNonQuery(ConStr, CommandType.Text, sql);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int ExecuteAsync(string sql, SqlParameter[] param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(ConStr, CommandType.Text, sql,param);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public CommonResponse getResponseBySp(string SpName)
        {
            try
            {
                DataTable result = (DataTable)SqlHelper.ExecuteScalar(ConStr,CommandType.Text, SpName);
                List<TEntity> lstData = ConvertDataTable<TEntity>(result);
                CommonResponse cr = new CommonResponse(lstData.Count > 0 ? HttpStatusCode.Accepted : HttpStatusCode.NoContent);
                cr.pageno = 0;
                cr.totalcount = 0;
                cr.totalSum = 0;                  
                cr.message = string.Empty;
                cr.results = lstData;
                return cr;
            }
            catch (Exception ex)
            {

                CommonResponse cr = new CommonResponse(HttpStatusCode.BadRequest);
                cr.results = null;
                cr.HasError = true;
                cr.message = ex.Message;
                return cr;
            }
          
        }
        public CommonResponse getDatasetResponseBySp(string SpName)
        {
            try
            {
             
                DataSet result = SqlHelper.ExecuteDataset(ConStr, SpName, null);                    
                CommonResponse cr = new CommonResponse(result.Tables.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent);
                cr.results = result;
                cr.pageno = 0;
                cr.totalcount = 0;
                cr.totalSum = 0;
                cr.message = string.Empty;
                cr.results = result;
                return cr;
            }
            catch (Exception ex)
            {

                CommonResponse cr = new CommonResponse(HttpStatusCode.BadRequest);
                cr.results = null;
                cr.HasError = true;
                cr.message = ex.Message;
                return cr;
            }

        }
        public CommonResponse getDatasetResponseBySp(string SpName, params object[] paramValues)
        {
            try
            {
                DataSet result = SqlHelper.ExecuteDataset(ConStr, SpName, paramValues);
                CommonResponse cr = new CommonResponse(result.Tables.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent);
                cr.results = result;
                cr.pageno = 0;
                cr.totalcount = 0;
                cr.totalSum = 0;
                cr.message = string.Empty;
                cr.results = result;
                return cr;
            }
            catch (Exception ex)
            {
                CommonResponse cr = new CommonResponse(HttpStatusCode.BadRequest);
                cr.results = null;
                cr.HasError = true;
                cr.message = ex.Message;
                return cr;
            }

        }

        public CommonResponse getResponseBySpWithParam(string SpName, params object[] parameterValues)
        {
            try
            {
                DataTable result = SqlHelper.ExecuteDataTable(ConStr, SpName, parameterValues);
                List<TEntity> lstData = ConvertDataTable<TEntity>(result); 
                CommonResponse cr = new CommonResponse(lstData.Count > 0 ? HttpStatusCode.Accepted : HttpStatusCode.NoContent);
                cr.pageno = 0;
                cr.totalcount = 0;
                cr.totalSum = 0;
                cr.message = string.Empty;
                cr.results = lstData;
                return cr;
            }
            catch (Exception ex)
            {

                CommonResponse cr = new CommonResponse(HttpStatusCode.BadRequest);
                cr.results = null;
                cr.HasError = true;
                cr.message = ex.Message;
                return cr;
            }

        }
        public CommonResponse GetDatatableBySQL(string SQL)
        {
            DataTable objValue;
            CommonResponse cr;
            try
            {
                objValue = SqlHelper.ExecuteDataTable(ConStr, CommandType.Text, SQL);
                List<TEntity> lstData = ConvertDataTable<TEntity>(objValue);
                 cr = new CommonResponse(lstData.Count > 0 ? HttpStatusCode.Accepted : HttpStatusCode.NoContent);
                cr.pageno = 0;
                cr.totalcount = 0;
                cr.totalSum = 0;
                cr.message = string.Empty;
                cr.results = lstData;
                return cr;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public CommonResponse GetDatatableBySQLGEN<T>(string SQL) where T : class
        {
            DataTable objValue;
            CommonResponse cr;
            try
            {
                objValue = SqlHelper.ExecuteDataTable(ConStr, CommandType.Text, SQL);
                List<T> lstData = ConvertDataTable<T>(objValue);
                cr = new CommonResponse(lstData.Count > 0 ? HttpStatusCode.Accepted : HttpStatusCode.NoContent);
                cr.pageno = 0;
                cr.totalcount = 0;
                cr.totalSum = 0;
                cr.message = string.Empty;
                cr.results = lstData;
                return cr;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public CommonResponse GetDatatableBySQL(string SQL,params SqlParameter[] sqlParameters)
        {
            DataTable objValue;
            CommonResponse cr;
            try
            {
                objValue = SqlHelper.ExecuteDataTable(ConStr, CommandType.Text, SQL, sqlParameters);
                List<TEntity> lstData = ConvertDataTable<TEntity>(objValue);
                cr = new CommonResponse(lstData.Count > 0 ? HttpStatusCode.Accepted : HttpStatusCode.NoContent);
                cr.pageno = 0;
                cr.totalcount = 0;
                cr.totalSum = 0;
                cr.message = string.Empty;
                cr.results = lstData;
                if (objValue.Rows.Count > 0)
                {
                    cr.status = 1;
                }
                return cr;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public TEntity GetSQL(string SQL)
        {
            DataTable objValue;
            TEntity lstData;
            CommonResponse cr;
            try
            {
                objValue = SqlHelper.ExecuteDataTable(ConStr, CommandType.Text, SQL);
              lstData = ConvertDataTable<TEntity>(objValue).FirstOrDefault();
             
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstData;
        }
        public string GetMaxIdGen(string coloumName, string rightStringFormat, string prefix, string tableName)
        {

            string maxId = "";

          string  sql = "SELECT ISNULL(RIGHT(MAX(" + coloumName + ")," + rightStringFormat.Length + ") , 0) +1  AS maxID  FROM  " + tableName + " where  left(" + coloumName + ", " + prefix.Length + ")='" + prefix + "' ";

            maxId = SqlHelper.ExecuteDataTable(ConStr, CommandType.Text, sql).Rows[0]["maxID"].ToString();

            decimal maxValue;
            decimal.TryParse(maxId, out maxValue);

            string returnValue = prefix+ maxValue.ToString(rightStringFormat);

            return returnValue;
        }
        public string GetMaxInoviceNoByShop(string tableName, string shopID, string prefix)
        {
            string id = "";
            string maxId = "";
            prefix = shopID + prefix;
            string selectQuery = "SELECT (ISNULL(RIGHT(MAX(InvoiceNo),6),0)+1) AS maxID FROM " + tableName + " WHERE ShopID='" + shopID + "'";
            id = SqlHelper.ExecuteDataTable(ConStr, CommandType.Text, selectQuery).Rows[0]["maxID"].ToString();
            //id = (decimal.Parse(initialValue) + decimal.Parse(id)).ToString();
            string initialValue = "000001";
            if (id.Length < initialValue.Length)
            {
                int prefixZero = initialValue.Length - id.Length;
                string _prefix = "";
                for (int i = 0; i < prefixZero; i++)
                {
                    _prefix += "0";
                }
                id = _prefix + id;
            }
            maxId = prefix + id;
            return maxId;
        }
        public string GetMaxId(string coloumName, string rightStringLength, string initialValue, string tableName, string prefix)
        {
            string id = "";
            string maxId = "";

           string selectQuery = "SELECT ISNULL(MAX(RIGHT(" + coloumName + ", " + rightStringLength + " )) +1, " + initialValue +" )  AS maxID " +
                              " FROM  " + tableName + " ";

            id = SqlHelper.ExecuteDataTable(ConStr, CommandType.Text, selectQuery).Rows[0]["maxID"].ToString();
            //id = (decimal.Parse(initialValue) + decimal.Parse(id)).ToString();

            if (id.Length < initialValue.Length)
            {
                int prefixZero = initialValue.Length - id.Length;
                string _prefix = "";
                for (int i = 0; i < prefixZero; i++)
                {
                    _prefix += "0";
                }
                id = _prefix + id;
            }
            maxId = prefix + id;
            return maxId;

        }
        #endregion
        #region Private Method
        public string ConStr
        {
            get
            {
                return Configuration.GetConnectionString("GGCorporateSaleContextConnection").ToString();
            }

        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    try
                    {

                    
                    if (pro.Name == column.ColumnName) {
                        var aa = dr[column.ColumnName];
                        if (dr[column.ColumnName] == DBNull.Value) {
                            if (column.DataType.Name == "Int32")
                            {
                                pro.SetValue(obj, 0, null);
                            }
                           else if (column.DataType.Name == "Decimal")
                            {
                                pro.SetValue(obj,Convert.ToDecimal(0.00), null);
                            }
                            else if (column.DataType.Name == "String")
                            {
                                pro.SetValue(obj, null, null);
                            }
                            else if (column.DataType.Name == "DateTime")
                            {
                                pro.SetValue(obj, null, null);
                            }
                            else if (column.DataType.Name.ToLower() == "datetimeoffset")
                            {
                                pro.SetValue(obj, null, null);
                            }
                            else if (column.DataType.Name.ToLower() == "byte[]")
                            {
                                pro.SetValue(obj, null, null);
                            }
                            
                            else if (column.DataType.Name == "Boolean")
                            {
                                pro.SetValue(obj, false, null);
                            }
                            else {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }
                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                    }
                    else
                        continue;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    };
                }
            }
            return obj;
        }

        



        #endregion
        public class FileLogger
        {
            public static void Log(string logInfo)
            {



                //var path = HttpContext.Current.Server.MapPath("~");
                //File.AppendAllText(path + "/Logger.txt", logInfo);
            }
        }
    }
}
