using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DALayer
{
    public class Utilitycs
    {
        public sealed class SQLHelper : IDisposable
        {
            private const int COMMAND_TIMEOUT = 30;

            private string mstrCN;

            private SqlConnection mCn; //conn

            private SqlCommand mCmd;
            private SqlTransaction mTrans;

            public SQLHelper()
            {
                mstrCN = System.Configuration.ConfigurationManager.AppSettings["ConectionString"].ToString();
            }

            // Default Constructor
            public SQLHelper(string connStr)
            {
                mstrCN = connStr;
            }


            #region Public Properties
            public string ConnectionString
            {
                get { return mstrCN; }
                set { mstrCN = value; }
            }

            public SqlConnection SQLConnection
            {
                get { return mCn; }
                set { mCn = value; }
            }

            public SqlCommand SQLCommand
            {
                get { return mCmd; }
                set { mCmd = value; }
            }
            public SqlTransaction SQLTransaction
            {
                get { return mTrans; }
                set { mTrans = value; }
            }
            #endregion

            #region Private Functions

            private void InitCommandForSP(string spName)
            {
                OpenConnection();
                mCmd = mCn.CreateCommand();
                mCmd.CommandTimeout = COMMAND_TIMEOUT;
                mCmd.CommandType = CommandType.StoredProcedure;
                mCmd.CommandText = spName;
            }

            private void InitCommandForFun(string spName)
            {
                OpenConnection();
                mCmd = mCn.CreateCommand();
                mCmd.CommandTimeout = COMMAND_TIMEOUT;
                mCmd.CommandType = CommandType.StoredProcedure;
                mCmd.CommandText = spName;
            }

            private void InitCommandForSQL(string sqlText)
            {
                OpenConnection();
                mCmd = mCn.CreateCommand();
                mCmd.CommandTimeout = COMMAND_TIMEOUT;
                mCmd.CommandType = CommandType.Text;
                mCmd.CommandText = sqlText;
            }

            #endregion

            public void OpenTransConnection()
            {
                CloseConnection();
                mCn = new SqlConnection(mstrCN);
                mCn.Open();
                mTrans = SQLConnection.BeginTransaction();
            }
            private void OpenConnection()
            {
                CloseConnection();
                mCn = new SqlConnection(mstrCN);
                mCn.ConnectionString = mstrCN;
                try
                {
                    mCn.Open();
                }
                catch (Exception ex)
                {
                    /*      mCn.ConnectionString = Connection.ReturnSecondaryConnectionString();
                          mCn.Open(); */
                }

            }
            private void CloseConnection()
            {
                if (mCn != null) // If the connection has already been set to a value
                {
                    if (mCn.State != ConnectionState.Closed) // If the connection is not closed.
                    {

                        mCn.Close();
                    }
                }
            }
            public void PrepareTransCommand(string spName, SqlParameter[] parms)
            {
                mCmd = mCn.CreateCommand();
                mCmd.Transaction = mTrans;
                mCmd.CommandTimeout = COMMAND_TIMEOUT;
                mCmd.CommandType = CommandType.StoredProcedure;
                mCmd.CommandText = spName;
                mCmd.Prepare();
                foreach (SqlParameter pr in parms)
                    if (pr != null)
                        mCmd.Parameters.Add(pr);
            }
            public void PrepareCommand(string spName, SqlParameter[] parms)
            {
                OpenConnection();
                mCmd = mCn.CreateCommand();
                mCmd.CommandTimeout = COMMAND_TIMEOUT;
                mCmd.CommandType = CommandType.StoredProcedure;
                mCmd.CommandText = spName;
                mCmd.Prepare();
                foreach (SqlParameter pr in parms)
                    if (pr != null)
                        mCmd.Parameters.Add(pr);
            }

            public void ExecutePrepared()
            {
                mCmd.ExecuteNonQuery();
            }
            /// <summary>
            /// Returns a SqlDataReader object with the results of the specified stored procedure
            /// </summary>
            /// <param name="spName">The name of the stored procedure to execute</param>
            /// <returns>SqlDataReader</returns>
            public SqlDataReader RunSpReturnDr(string spName)
            {
                SqlDataReader dr = null;
                InitCommandForSP(spName);

                using (dr)
                {
                    dr = mCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    mCmd.Dispose();
                    return dr;
                }
            }
            /// <summary>
            /// Returns a SqlDataReader object with the results of the specified stored procedure
            /// </summary>
            /// <param name="spName">The name of the stored procedure to execute</param>
            /// <param name="parms">Oracle Parameter array</param>
            /// <returns>SqlDataReader</returns>
            public SqlDataReader RunSpReturnDr(string spName, SqlParameter[] parms)
            {
                SqlDataReader DR = null;
                InitCommandForSP(spName);

                foreach (SqlParameter pr in parms)
                    if (pr != null)
                        mCmd.Parameters.Add(pr);

                using (DR)
                {
                    DR = mCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    mCmd.Dispose();
                    return DR;
                }
            }
            /// <summary>
            /// Returns a dataset representing the query result set
            /// </summary>
            /// <param name="spName">The name of the stored procedure to execute</param>
            /// <returns>DataSet</returns>
            public DataSet RunSpReturnDs(string spName)
            {
                DataSet ds = new DataSet();
                InitCommandForSP(spName);

                using (mCmd)
                {
                    SqlDataAdapter da = new SqlDataAdapter(mCmd);
                    da.Fill(ds);
                    mCmd.Connection.Close();
                    da.Dispose();
                }

                return ds;
            }

            /// <summary>
            /// Returns a dataset representing the query result set
            /// </summary>
            /// <param name="spName">The name of the stored procedure to execute</param>
            /// <param name="parms">Oracle Parameter array</param>
            /// <returns>DataSet</returns>
            public DataSet RunSpReturnDs(string spName, SqlParameter[] parms)
            {
                DataSet ds = new DataSet();
                InitCommandForSP(spName);

                using (mCmd)
                {
                    SqlDataAdapter da = new SqlDataAdapter(mCmd);
                    foreach (SqlParameter pr in parms)
                        if (pr != null)
                            mCmd.Parameters.Add(pr);

                    da.Fill(ds);
                    mCmd.Connection.Close();
                    da.Dispose();
                }

                return ds;
            }


            public DataSet RunFunReturnDs(string spName, SqlParameter[] parms)
            {
                DataSet ds = new DataSet();
                InitCommandForSP(spName);

                using (mCmd)
                {
                    SqlDataAdapter da = new SqlDataAdapter(mCmd);
                    foreach (SqlParameter pr in parms)
                        if (pr != null)
                            mCmd.Parameters.Add(pr);

                    da.Fill(ds);
                    mCmd.Connection.Close();
                    da.Dispose();
                }

                return ds;
            }

            /// <summary>
            /// Returns a SqlDataReader object with the results of the specified SQL statement
            /// </summary>
            /// <param name="Oracle">SQL statement</param>
            /// <returns>SqlDataReader</returns>
            public SqlDataReader RunSqlReturnDr(string Sql)
            {
                SqlDataReader DR = null;
                InitCommandForSQL(Sql);

                using (mCmd)
                {
                    DR = mCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    mCmd.Dispose();
                    return DR;
                }
            }

            /// <summary>
            /// Executes the SQL query string 
            /// </summary>
            /// <param name="Sql">The SQL query string to execute</param>
            /// <returns>Object</returns>
            public object RunSqlReturnScalar(string Sql)
            {
                InitCommandForSQL(Sql);
                object obj;
                using (mCmd)
                {
                    obj = mCmd.ExecuteScalar();
                    mCmd.Connection.Close();
                    mCmd.Dispose();
                }
                return obj;
            }

            /// <summary>
            /// Executes the SQL query string 
            /// </summary>
            /// <param name="Sql">The SQL query string to execute</param>
            /// <returns>Number of rows affected as an Int values </returns>
            public int RunSql(string Sql)
            {

                //InitCommandForSQL(Sql);
                //int iRtnVal;
                //using (mCmd)
                //{

                //    mTrans = mCn.BeginTransaction();
                //    mCmd.Transaction = mTrans;
                //    iRtnVal = mCmd.ExecuteNonQuery();
                //    mTrans.Commit();
                //    mTrans.Dispose();
                //    mCmd.Connection.Close();//shiv
                //    mCmd.Dispose();
                //}

                //return iRtnVal;

                InitCommandForSQL(Sql);
                int iRtnVal;
                using (mCmd)
                {
                    iRtnVal = mCmd.ExecuteNonQuery();
                    mCmd.Connection.Close();
                    mCmd.Dispose();
                }
                return iRtnVal;

            }

            /// <summary>
            /// Returns a dataset representing the query result set
            /// </summary>
            /// <param name="spName">The SQL string to execute</param>
            /// <returns>DataSet</returns>
            public DataSet RunSqlReturnDs(string Sql)
            {
                DataSet ds = new DataSet();
                InitCommandForSQL(Sql);

                using (mCmd)
                {
                    SqlDataAdapter da = new SqlDataAdapter(mCmd);
                    da.Fill(ds);
                    mCmd.Connection.Close();
                    da.Dispose();
                }
                return ds;
            }

            /// <summary>
            /// Returns a dataset representing the query result set (Allows for custom paging)
            /// </summary>
            /// <param name="spName">The SQL string to execute</param>
            /// <param name="CurrentIndex">int Beginning record</param>
            /// <param name="PageSize">int Records allowed per page</param>
            /// <param name="SourceTable">string Source table name</param>
            /// <returns>DataSet</returns>
            public DataSet RunSqlReturnDs(string Sql, int CurrentIndex, int PageSize, string SourceTable)
            {
                DataSet ds = new DataSet();
                InitCommandForSQL(Sql);

                using (mCmd)
                {
                    SqlDataAdapter da = new SqlDataAdapter(mCmd);
                    da.Fill(ds, CurrentIndex, PageSize, SourceTable);
                    mCmd.Connection.Close();
                    da.Dispose();
                }

                return ds;
            }

            /// <summary>
            /// Returns a dataset representing the query result set
            /// </summary>
            /// <param name="Sql">The Sql query to execute</param>
            /// <param name="parms">Sql Parameter array</param>
            /// <returns>Object</returns>
            public DataSet RunSqlReturnDs(string Sql, SqlParameter[] parms)
            {
                DataSet ds = new DataSet();
                InitCommandForSQL(Sql);

                using (mCmd)
                {
                    foreach (SqlParameter pr in parms)
                        if (pr != null)
                            mCmd.Parameters.Add(pr);

                    SqlDataAdapter da = new SqlDataAdapter(mCmd);
                    da.Fill(ds);
                    mCmd.Connection.Close();
                    da.Dispose();

                }
                return ds;
            }

            /// <summary>
            /// Returns the number of records affected by the specified stored procedure
            /// </summary>
            /// <param name="sSpName">The name of the stored procedure to execute</param>
            /// <param name="parms">Sql Parameter array</param>
            /// <returns>int</returns>
            public int RunSp(string spName, SqlParameter[] parms)
            {
                InitCommandForSP(spName);
                int rtnVal = 0;

                using (mCmd)
                {
                    foreach (SqlParameter pr in parms)
                        if (pr != null)
                            mCmd.Parameters.Add(pr);

                    rtnVal = mCmd.ExecuteNonQuery();
                    mCmd.Connection.Close();
                }
                return rtnVal;
            }

            /// <summary>
            /// Returns the number of records affected by the specified stored procedure
            /// </summary>
            /// <param name="spName"></param>
            /// <returns></returns>
            public int RunSp(string spName)
            {
                InitCommandForSP(spName);
                int rtnVal = 0;

                using (mCmd)
                {
                    rtnVal = mCmd.ExecuteNonQuery();
                    mCmd.Connection.Close();
                }

                return rtnVal;
            }

            /// <summary>
            /// Executes the query, and returns the first column of the first row in the result set returned by the query. Extra columns or rows are ignored.
            /// </summary>
            /// <param name="spName">The name of the stored procedure to execute</param>
            /// <param name="parms">Parameters required by the stored procedure.</param>
            /// <returns></returns>
            //public object RunSpReturnScalar2(string spName, SqlParameter[] parms)
            //{
            //    object rtnVal = null;
            //    InitCommandForSP(spName);

            //    using (mCmd)
            //    {
            //        foreach (SqlParameter pr in parms)
            //            if (pr != null)
            //                mCmd.Parameters.Add(pr);

            //         //rtnVal = mCmd.ExecuteScalar();

            //       rtnVal = mCmd.ExecuteOracleScalar();

            //        mCmd.Connection.Close();
            //    }

            //    return rtnVal;
            //}


            public object RunSpReturnScalar(string spName, SqlParameter[] parms)
            {
                DataSet ds = new DataSet();
                InitCommandForSP(spName);

                using (mCmd)
                {
                    SqlDataAdapter da = new SqlDataAdapter(mCmd);
                    foreach (SqlParameter pr in parms)
                        if (pr != null)
                            mCmd.Parameters.Add(pr);

                    da.Fill(ds);
                    mCmd.Connection.Close();
                    da.Dispose();
                }
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0][0].ToString().Trim();
                    }
                    else
                        return "";
                }
                else
                    return "";
            }

            public int RunSql(string Sql, SqlParameter[] parms)
            {
                InitCommandForSQL(Sql);
                int rtnVal = 0;

                using (mCmd)
                {
                    foreach (SqlParameter pr in parms)
                        if (pr != null)
                            mCmd.Parameters.Add(pr);

                    rtnVal = mCmd.ExecuteNonQuery();
                    mCmd.Connection.Close();
                }
                return rtnVal;
            }

            #region IDisposable Members

            public void Dispose()
            {
                if (mCmd != null)
                    mCmd.Dispose();
                CloseConnection();
            }

            #endregion
        }

    }
}