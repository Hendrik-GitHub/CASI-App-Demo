using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using OneStopShop.API.Models.Logging;
using System.Data.Common;
using System.Data;
using Npgsql;

namespace OneStopShop.API.Services.Logging
{
    public class LoggingRepository
    {
        private string _connectionString { get; set; }

        public LoggingRepository(string ConnectionStr)
        {
            _connectionString = ConnectionStr;
        }

        public bool InsertLog(Log log)
        {
            bool bResult = false;

            try
            {

                #region PostgreSQL WriteLog Procedure

                NpgsqlConnection pgcon = new NpgsqlConnection(_connectionString);
                pgcon.Open();

                NpgsqlCommand pgcom = new NpgsqlCommand("call writelog(:eventid, :priority, :severity, :title, :machinename, :appdomainname, :processid, :processname, :threadname, :win32threadid, :message, :formattedmessage)", pgcon);

                pgcom.CommandType = CommandType.Text;

                pgcom.Parameters.AddWithValue("eventid", DbType.Int32).Value = log.eventid;
                pgcom.Parameters.AddWithValue("priority", DbType.Int32).Value = log.priority;
                pgcom.Parameters.AddWithValue("severity", DbType.String).Value = log.severity;
                pgcom.Parameters.AddWithValue("title", DbType.String).Value = log.title;
                pgcom.Parameters.AddWithValue("machinename", DbType.String).Value = log.machinename;
                pgcom.Parameters.AddWithValue("appdomainname", DbType.String).Value = log.appdomainname;
                pgcom.Parameters.AddWithValue("processid", DbType.String).Value = log.processid;
                pgcom.Parameters.AddWithValue("processname", DbType.String).Value = log.processname;

                if (log.threadname == null)
                {
                    log.threadname = "";
                }

                pgcom.Parameters.AddWithValue("threadname", DbType.String).Value = log.threadname;
                pgcom.Parameters.AddWithValue("win32threadid", DbType.String).Value = log.win32threadid;
                pgcom.Parameters.AddWithValue("message", DbType.String).Value = log.win32threadid;
                pgcom.Parameters.AddWithValue("formattedmessage", DbType.String).Value = log.formattedmessage;

                int rows = pgcom.ExecuteNonQuery();
                bResult = true;

                #endregion
            }
            catch (Exception ex)
            {
                return false;
            }

            return bResult;
        }
    }
}
