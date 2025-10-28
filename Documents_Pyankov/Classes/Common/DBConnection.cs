using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documents_Pyankov.Classes.Common
{
    public class DBConnection
    {
        public static readonly string Path = @"C:\Users\vladi\source\repos\Documents_Pyankov\Documents_Pyankov\bin\Debug\DataBase.accdb";

        public static OleDbConnection Connection()
        {
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Path);
            connection.Open();
            return connection;
        }
        public static OleDbDataReader Query(string sql, OleDbConnection connection)
        {
            OleDbCommand command = new OleDbCommand(sql, connection);
            return command.ExecuteReader();
        }
        public static void CloseConnection(OleDbConnection connection)
        {
            connection.Close();
        }
    }
}
