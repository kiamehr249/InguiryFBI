using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace FbiInquiry.Utilities
{
    public class ADOUtilities
    {
        private string _connection;
        public ADOUtilities(string conn)
        {
            _connection = conn;
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connection);
            return connection;
        }

        public DataTable ExecuteCommand(string command)
        {
            command = command.Replace('\x06A9', '\x0643');
            command = command.Replace('\x064a', '\x06cc');
            command = command.Replace('\x0649', '\x06cc');

            var table = new DataTable();
            var conn = GetConnection();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            var data = new SqlDataAdapter(command, conn);
            data.Fill(table);
            conn.Close();
            return table;
        }

        protected bool NonQuery(string sql)
        {
            sql = sql.Replace('\x06A9', '\x0643');
            sql = sql.Replace('\x064a', '\x06cc');
            sql = sql.Replace('\x0649', '\x06cc');
            var cmd = new SqlCommand(sql, GetConnection());
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return true;
        }

        public bool ExecNonQuery(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                throw new Exception("NonQuery : Empty Query");
            return NonQuery(sql);
        }


        private SqlDataReader Query(string sql)
        {
            sql = sql.Replace('\x06A9', '\x0643').Replace('\x064a', '\x06cc').Replace('\x0649', '\x06cc');
            var cmd = new SqlCommand(sql, GetConnection());
            SqlDataReader dtr;
            try
            {
                cmd.Connection.Open();
                dtr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return dtr;
        }



    }
}
