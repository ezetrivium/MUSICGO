using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Utilities;
using BE;
using BE.Entities;

namespace DAL
{
    public class DBContext
    {
        private SqlConnection conexion;
        private SqlTransaction transaccion;
        //private Server server;

        private void OpenConnection()
        {
            if (conexion == null)
            {
                var conString = GlobalValues.WebConnectionString;
                conexion = new SqlConnection(conString);
                conexion.Open();
            }
        }

        private void CloseConnection()
        {
            if (transaccion == null)
            {
                conexion.Close();
                conexion.Dispose();
            }
        }

        public DataSet Read(string spName, SqlParameter[] sqlParameters = null)
        {
            try
            {
                OpenConnection();
                var dataSet = new DataSet();
                using (var dataAdapter = new SqlDataAdapter())
                {
                    using (dataAdapter.SelectCommand = new SqlCommand
                    {
                        CommandText = spName,
                        CommandType = CommandType.StoredProcedure,
                        Connection = conexion
                    })
                    {
                        if (sqlParameters != null && sqlParameters.Length > 0)
                            dataAdapter.SelectCommand.Parameters.AddRange(sqlParameters);

                        if (transaccion != null)
                            dataAdapter.SelectCommand.Transaction = transaccion;
                    }
                    dataAdapter.Fill(dataSet);
                }
                CloseConnection();

                return dataSet;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public int Write(string spName, SqlParameter[] sqlParameters)
        {
            try
            {
                var rowAffected = 0;
                OpenConnection();
                using (var sqlCommand = new SqlCommand
                {
                    CommandText = spName,
                    CommandType = CommandType.StoredProcedure,
                    Connection = conexion
                })
                {
                    sqlCommand.Parameters.AddRange(sqlParameters);

                    if (transaccion != null)
                        sqlCommand.Transaction = transaccion;

                    try
                    {
                        rowAffected = sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        rowAffected = -1;
                    }
                }
                CloseConnection();

                return rowAffected;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        #region Transacciones
        public void BeginTran()
        {
            if (conexion == null || conexion.State != ConnectionState.Open)
                OpenConnection();

            transaccion = conexion.BeginTransaction();
        }

        public void CommitTran()
        {
            transaccion.Commit();
            transaccion.Dispose();
            CloseConnection();
        }

        public void RollBackTran()
        {
            transaccion.Rollback();
            transaccion.Dispose();
            CloseConnection();
        }

        #endregion

        #region SMO Management
        public int MakeBackup(SqlParameter[] sqlParameters)
        {
            try
            {
                var rowAffected = 0;
                OpenConnection();

                using (var sqlCommand = new SqlCommand
                {
                    CommandText = "MakeBackup",
                    CommandType = CommandType.StoredProcedure,
                    Connection = conexion
                })
                {
                    sqlCommand.Parameters.AddRange(sqlParameters);

                    try
                    {
                        rowAffected = sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        rowAffected = -1;
                    }
                }
                CloseConnection();

                return rowAffected;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int MakeRestore(SqlParameter[] sqlParameters)
        {
            try
            {
                var rowAffected = 0;
                

                var conString = GlobalValues.ConnectionStringMaster;
                conexion = new SqlConnection(conString);
                conexion.Open();

                using (var sqlCommand = new SqlCommand
                {
                    CommandText = "MakeRestore",
                    CommandType = CommandType.StoredProcedure,
                    Connection = conexion
                })
                {
                    sqlCommand.Parameters.AddRange(sqlParameters);

                    try
                    {
                        rowAffected = sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        rowAffected = 0;
                    }
                }
                conexion.Close();
                conexion.Dispose();

                return rowAffected;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Parameters
        public SqlParameter CreateParameters(string paramName, string paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                SqlDbType = SqlDbType.NVarChar
            };

            return parameter;
        }


        public SqlParameter CreateParameters(string paramName, Guid paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                SqlDbType = SqlDbType.UniqueIdentifier
            };

            return parameter;
        }

        public SqlParameter CreateNullParameters(string paramName)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = DBNull.Value,
                SqlDbType = SqlDbType.NVarChar
            };

            return parameter;
        }

        public SqlParameter CreateParameters(string paramName, int paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                SqlDbType = SqlDbType.Int
            };

            return parameter;
        }

        public SqlParameter CreateParameters(string paramName, DateTime paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                SqlDbType = SqlDbType.DateTime
            };

            return parameter;
        }
        public SqlParameter CreateParameters(string paramName, DateTime? paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                SqlDbType = SqlDbType.DateTime
            };

            return parameter;
        }

        public SqlParameter CreateParameters(string paramName, bool paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                SqlDbType = SqlDbType.Bit
            };

            return parameter;
        }

        public SqlParameter CreateParameters(string paramName, decimal paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue,
                SqlDbType = SqlDbType.Decimal
            };

            return parameter;
        }

        public SqlParameter CreateParameters(string paramName, DataTable paramValue)
        {
            var parameter = new SqlParameter
            {
                ParameterName = paramName,
                Value = paramValue
            };

            return parameter;
        }
        #endregion
    }
}