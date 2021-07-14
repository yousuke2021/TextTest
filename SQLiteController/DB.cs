using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteController
{
    public class DB
    {
        protected string path;
        protected Dictionary<string, Table> tables = new();

        public string Path { get { return path; } }

        public DB(string path)
        {
            this.path = path;
        }

        public DB(string path, params Table[] tables)
        {
            this.path = path;
            AddTable(tables);
        }

        public DB(string path, params (string key, Table value)[] tables)
        {
            this.path = path;
            AddTable(tables);
        }

        public void AddTable(params Table[] tables)
        {
            foreach (var table in tables)
            {
                this.tables.Add(table.Name, table);
            }
        }

        public void AddTable(params (string key, Table value)[] tables)
        {
            foreach (var table in tables)
            {
                this.tables.Add(table.key, table.value);
            }
        }

    }
}
