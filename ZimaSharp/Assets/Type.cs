using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Assets
{
    public class Type
    {
        private int id;
        private string name;
        private int order;
        private int parent;
        private string base_text;
        private bool canuse;
        private bool uncountable;
        private string key_db;
        private string key_table;
        private string key_column;
        private string display_table;
        private string display_column;
        private string template;

        private List<Noun> nouns = new();
        private List<Type> arguments = new();
        private List<Attribute> attributes = new();

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public int Order { get { return order; } }
        public int Parent { get { return parent; } }
        public string Base { get { return base_text; } }
        public bool CanUse { get { return canuse; } }
        public bool UnCountable { get { return uncountable; } }
        public string KeyDB { get { return key_db; } }
        public string KeyTable { get { return key_table; } }
        public string KeyColumn { get { return key_column; } }
        public string DisplayTable { get { return display_table; } }
        public string DisplayColumn { get { return display_column; } }
        public string Template { get { return template; } }

        public List<Noun> Nouns { get { return new List<Noun>(nouns); } }
        public List<Type> Arguments { get { return new List<Type>(arguments); } }
        public List<Attribute> Attributes { get { return new List<Attribute>(attributes); } }

        public Type(int id, string name, int order, int parent, string base_text, bool canuse, bool uncountable, 
            string key_db, string key_table, string key_column, string display_table, string display_column, string template)
        {
            this.id = id;
            this.name = name;
            this.order = order;
            this.parent = parent;
            this.base_text = base_text;
            this.canuse = canuse;
            this.uncountable = uncountable;
            this.key_db = key_db;
            this.key_table = key_table;
            this.key_column = key_column;
            this.display_table = display_table;
            this.display_column = display_column;
            this.template = template;
        }

        public Type(SQLiteDataReader result)
        {
            id = Convert.ToInt32(result[ZimaDB.TYPE.ID].ToString());
            name = result[ZimaDB.TYPE.NAME].ToString();
            order = Convert.ToInt32(result[ZimaDB.TYPE.ORDER].ToString());
            parent = Convert.ToInt32(result[ZimaDB.TYPE.PARENT].ToString());
            base_text = result[ZimaDB.TYPE.BASE].ToString();
            canuse = Convert.ToBoolean(result[ZimaDB.TYPE.CANUSE].ToString());
            uncountable = Convert.ToBoolean(result[ZimaDB.TYPE.UNCOUNTABLE].ToString());
            key_db = result[ZimaDB.TYPE.KEYDB].ToString();
            key_table = result[ZimaDB.TYPE.KEYTABLE].ToString();
            display_table = result[ZimaDB.TYPE.DISPLAYTABLE].ToString();
            display_column = result[ZimaDB.TYPE.DISPLAYCOLUMN].ToString();
            template = result[ZimaDB.TYPE.TEMPLATE].ToString();
        }


        public void AddNoun(params Noun[] nouns)
        {
            foreach(var noun in nouns)
            {
                this.nouns.Add(noun);
            }
        }

        public void AddArgument(params Type[] arguments)
        {
            foreach(var argument in arguments)
            {
                int count = this.arguments.Count;
                bool addflag = false;
                for(int i = 0; i < count; i++)
                {
                    if(argument.Order <= this.arguments[i].Order)
                    {
                        this.arguments.Insert(i, argument);
                        addflag = true;
                    }
                }
                if (!addflag)
                {
                    this.arguments.Add(argument);
                }
            }
        } 

        public void AddAttribute(params Attribute[] attributes)
        {
            foreach(var attribute in attributes)
            {
                int count = this.attributes.Count;
                bool addflag = false;
                for(int i = 0; i < count; i++)
                {
                    if(attribute.Order <= this.attributes[i].Order)
                    {
                        this.attributes.Insert(i, attribute);
                        addflag = true;
                    }
                }
                if (!addflag)
                {
                    this.attributes.Add(attribute);
                }
            }
        }
    }
}
