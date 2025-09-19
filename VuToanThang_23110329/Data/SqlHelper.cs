using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VuToanThang_23110329.Data
{
    public class SqlHelper
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["HrDb"].ConnectionString;

        /// <summary>
        /// Execute a stored procedure or SQL command that returns no result
        /// </summary>
        public static int ExecuteNonQuery(string commandText, params SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = new SqlCommand(commandText, connection))
                    {
                        // Determine if it's a stored procedure
                        if (!commandText.Trim().ToUpper().StartsWith("SELECT") && 
                            !commandText.Trim().ToUpper().StartsWith("INSERT") && 
                            !commandText.Trim().ToUpper().StartsWith("UPDATE") && 
                            !commandText.Trim().ToUpper().StartsWith("DELETE"))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                        }

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        connection.Open();
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing command: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Execute a stored procedure or SQL command that returns a single value
        /// </summary>
        public static object ExecuteScalar(string commandText, params SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = new SqlCommand(commandText, connection))
                    {
                        // Determine if it's a stored procedure
                        if (!commandText.Trim().ToUpper().StartsWith("SELECT") && 
                            !commandText.Trim().ToUpper().StartsWith("INSERT") && 
                            !commandText.Trim().ToUpper().StartsWith("UPDATE") && 
                            !commandText.Trim().ToUpper().StartsWith("DELETE"))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                        }

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        connection.Open();
                        return command.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing scalar command: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Execute a stored procedure or SQL command that returns a DataTable
        /// </summary>
        public static DataTable ExecuteDataTable(string commandText, params SqlParameter[] parameters)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    using (var command = new SqlCommand(commandText, connection))
                    {
                        // Determine if it's a stored procedure
                        if (!commandText.Trim().ToUpper().StartsWith("SELECT") && 
                            !commandText.Trim().ToUpper().StartsWith("WITH"))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                        }

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing data table command: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Execute a stored procedure or SQL command that returns a SqlDataReader
        /// </summary>
        public static SqlDataReader ExecuteReader(string commandText, params SqlParameter[] parameters)
        {
            try
            {
                var connection = new SqlConnection(ConnectionString);
                var command = new SqlCommand(commandText, connection);

                // Determine if it's a stored procedure
                if (!commandText.Trim().ToUpper().StartsWith("SELECT") && 
                    !commandText.Trim().ToUpper().StartsWith("WITH"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                }

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                connection.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing reader command: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Create a SqlParameter
        /// </summary>
        public static SqlParameter CreateParameter(string parameterName, object value)
        {
            return new SqlParameter(parameterName, value ?? DBNull.Value);
        }

        /// <summary>
        /// Create a SqlParameter with specific SqlDbType
        /// </summary>
        public static SqlParameter CreateParameter(string parameterName, SqlDbType dbType, object value)
        {
            return new SqlParameter(parameterName, dbType) { Value = value ?? DBNull.Value };
        }

        /// <summary>
        /// Create an output SqlParameter
        /// </summary>
        public static SqlParameter CreateOutputParameter(string parameterName, SqlDbType dbType)
        {
            return new SqlParameter(parameterName, dbType) { Direction = ParameterDirection.Output };
        }

        /// <summary>
        /// Test database connection
        /// </summary>
        public static bool TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
