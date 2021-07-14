using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Assets
{
    public class Noun
    {
        private int type_id;
        private string key;
        private string display_str;

        public int TypeId { get { return type_id; } }
        public string Key { get { return key; } }
        public string DisplayStr { get { return display_str; } }

        public Noun(int type_id, string key, string display_str)
        {
            this.type_id = type_id;
            this.key = key;
            this.display_str = display_str;
        }

        public Noun(SQLiteDataReader result)
        {
            type_id = Convert.ToInt32(result[ZimaDB.NOUN.TYPEID].ToString());
            key = result[ZimaDB.NOUN.KEY].ToString();
            display_str = result[ZimaDB.NOUN.DISPLAYSTR].ToString();
        }
    }
}
