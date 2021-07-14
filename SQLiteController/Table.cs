using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteController
{
    public class Table
    {
        private string name;
        private Dictionary<string, string> columns = new();

        public string Name { get { return name; } }

        public Dictionary<string, string> Columns{ get{ return new Dictionary<string, string>(columns); } }


        public Table(string name, params string[] columns)
        {
            this.name = name;
            foreach(var column in columns)
            {
                this.columns.Add(column, column);
            }
        }

        public Table(string name, params (string key, string value)[] columns)
        {
            this.name = name;
            foreach (var column in columns)
            {
                this.columns.Add(column.key, column.value);
            }
        }

        public bool CheckColumnsExists(params string[] check_columns)
        {
            foreach(var check_column in check_columns)
            {
                if (!columns.Keys.Contains(check_column))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
