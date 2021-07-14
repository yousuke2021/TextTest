using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteController
{
    public class SQL
    {
        public abstract class Command
        {
            private string command;

            public string Self { get { return command; } }

            public Command(string command)
            {
                this.command = command;
            }
        }

        public class SelectCommand : Command
        {
            public SelectCommand(string command) : base(command)
            {

            }
        }


        private DB db;

        public SQL()
        {
            this.db = null;
        }

        public SQL(DB db)
        {
            this.db = db;
        }

        public static SelectCommand SelectData(TargetData data)
        {
            string sql = "SELECT ";
            bool first = true;
            foreach(var column in data.TargetColumns)
            {
                if (!first)
                {
                    sql += ", ";
                }
                sql += data.Table.Columns[column];
                first = false;
            }
            sql += " FROM " + data.Table.Name;

            return new SelectCommand(sql);
        }
    }
}
