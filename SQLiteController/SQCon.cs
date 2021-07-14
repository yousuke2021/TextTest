using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteController
{
    public class SQCon
    {
        protected SQLiteConnection conn;
        protected DB db;

        public static string HelloWorld()
        {
            return "Hello SQLiteController";
        }

        public SQCon(DB db)
        {
            this.db = db;
            conn = new SQLiteConnection("Data Source = " + db.Path);
        }

        public void Open()
        {
            conn.Open();
        }

        public void Close()
        {
            conn.Close();
        }

        public IEnumerable<SQLiteDataReader> GetData(string sql)
        {
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    yield return reader;
                }
            }
        }

        public IEnumerable<SQLiteDataReader> GetData(SQL.SelectCommand command)
        {
            string sql = command.Self;
            foreach (var data in GetData(sql))
            {
                yield return data;
            }
        }

        public IEnumerable<SQLiteDataReader> GetAllData(Table table)
        {
            string sql = "SELECT * FROM " + table.Name;

            foreach(var data in GetData(sql))
            {
                yield return data;
            }
        }

        public IEnumerable<SQLiteDataReader> GetAllData(string table_name)
        {
            string sql = "SELECT * FROM " + table_name;

            foreach (var data in GetData(sql))
            {
                yield return data;
            }
        }
    }
}
