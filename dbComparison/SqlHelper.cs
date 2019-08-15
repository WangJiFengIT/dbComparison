using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbComparison
{
    public class SqlHelper
    {
        private static SqlConnection sqlConnection;

        #region SQL
        private static readonly string connectionString = "Data Source={0};Initial Catalog={1};User ID={2};Password={3}";
        private static readonly string sqlGetDataBases = "select * from sys.databases";
        private static readonly string sqlGetObjects = "select * from {0}.sys.objects";
        private static readonly string sqlGetTables = sqlGetObjects + " where type='U'";
        private static string sqlUseDataBase = string.Empty;//记录进入了哪个数据库
        private static string sqlUesTable = string.Empty;//记录进入了哪个表
        private static readonly string QUOTED_IDENTIFIER = "SET QUOTED_IDENTIFIER OFF";//让SQL启用双引号
        private static readonly string sqlGetTableInfo = QUOTED_IDENTIFIER + @"
                                                                                SELECT 
                                                                                CASE WHEN col.colorder = 1 THEN obj.name ELSE '' END AS 表名,
                                                                                col.colorder AS 序号,
                                                                                col.name AS 列名,
                                                                                ISNULL(ep.[value], '') AS 列说明,
                                                                                t.name AS 数据类型,
                                                                                col.length AS 长度,
                                                                                ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS 小数位数,
                                                                                CASE WHEN COLUMNPROPERTY(col.id, col.name, 'IsIdentity') = 1 THEN '√' ELSE '' END AS 标识 ,
                                                                                CASE WHEN EXISTS (SELECT 1
	                                                                                FROM dbo.sysindexes si
	                                                                                INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id
	                                                                                AND si.indid = sik.indid
	                                                                                INNER JOIN dbo.syscolumns sc ON sc.id = sik.id
	                                                                                AND sc.colid = sik.colid
	                                                                                INNER JOIN dbo.sysobjects so ON so.name = si.name
	                                                                                AND so.xtype = 'PK'
	                                                                                WHERE sc.id = col.id
	                                                                                AND sc.colid = col.colid) THEN '√' ELSE '' END AS 主键,
                                                                                CASE WHEN col.isnullable = 1 THEN '√' ELSE '' END AS 允许空,
                                                                                ISNULL(comm.text, '') AS 默认值
                                                                                FROM dbo.syscolumns col
                                                                                LEFT JOIN dbo.systypes t ON col.xtype = t.xusertype
                                                                                INNER JOIN dbo.sysobjects obj ON col.id = obj.id
                                                                                AND obj.xtype = 'U'
                                                                                AND obj.status >= 0
                                                                                LEFT JOIN dbo.syscomments comm ON col.cdefault = comm.id
                                                                                LEFT JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                                                AND col.colid = ep.minor_id
                                                                                AND ep.name = 'MS_Description'
                                                                                LEFT JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id
                                                                                AND epTwo.minor_id = 0
                                                                                AND epTwo.name = 'MS_Description'
                                                                                WHERE obj.name = " + "\"{0}\"" + @"
                                                                                ORDER BY col.colorder;";
        #endregion

        private static object obj = new object();
        public SqlHelper() { }
        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <param name="DataSource">数据库地址</param>
        /// <param name="UserId">用户名</param>
        /// <param name="Paassword">密码</param>
        /// <param name="DataBase">数据库名称【默认：master】</param>
        public static void Open(string DataSource, string UserId, string Paassword, string DataBase = "master")
        {
            sqlConnection = new SqlConnection(string.Format(connectionString, DataSource, DataBase, UserId, Paassword));
            sqlConnection.Open();
        }
        /// <summary>
        /// 异步打开数据库
        /// </summary>
        /// <param name="DataSource">数据库地址</param>
        /// <param name="UserId">用户名</param>
        /// <param name="Paassword">密码</param>
        /// <param name="DataBase">数据库名称【默认：master】</param>
        public static async void OpenAsync(string DataSource, string UserId, string Paassword, string DataBase = "master")
        {
            sqlConnection = new SqlConnection(string.Format(connectionString, DataSource, DataBase, UserId, Paassword));
            await sqlConnection.OpenAsync();
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public static void Close()
        {
            if (sqlConnection != null && sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }
        /// <summary>
        /// 获取所有数据库
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDataBase()
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlGetDataBases, sqlConnection);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            return dt;
        }
        /// <summary>
        /// 获取数据库中所有表
        /// </summary>
        /// <param name="DataBaseName">数据库名称</param>
        /// <returns></returns>
        public static DataTable GetTables(string DataBaseName)
        {
            sqlUseDataBase = $"USE {DataBaseName} ";
            return GetDataTable(string.Format(sqlGetTables, DataBaseName));
        }
        public static DataTable GetTableInfo(string TableName)
        {
            sqlUesTable = TableName;
            return GetDataTable(sqlUseDataBase + string.Format(sqlGetTableInfo, TableName));
        }
        /// <summary>
        /// 执行SQL返回DataSet
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string sqlQuery)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, sqlConnection);
                sqlDataAdapter.Fill(ds);
            }
            catch { }
            return ds;
        }
        /// <summary>
        /// 执行SQL返回DataTable
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sqlQuery)
        {
            DataSet ds = GetDataSet(sqlQuery);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
    }
}
