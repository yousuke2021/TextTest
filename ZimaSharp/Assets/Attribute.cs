using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Assets
{
    public class Attribute
    {
        private int type_id;
        private string name;
        private int attribute_type;
        private string display_str;
        private int order;

        public int TypeId { get { return type_id; } }
        public string Name { get { return name; } }
        public int AttributeType { get { return attribute_type; } }
        public string DisplayStr { get { return display_str; } }
        public int Order { get { return order; } }


        public Attribute(int type_id, string name, int attribute_type, string display_str, int order)
        {
            this.type_id = type_id;
            this.name = name;
            this.attribute_type = attribute_type;
            this.display_str = display_str;
            this.order = order;
        }

        public Attribute(SQLiteDataReader result)
        {
            type_id = Convert.ToInt32(result[ZimaDB.ATTRIBUTE.TYPEID].ToString());
            name = result[ZimaDB.ATTRIBUTE.NAME].ToString();
            attribute_type = Convert.ToInt32(result[ZimaDB.ATTRIBUTE.ATTRIBUTETYPE].ToString());
            display_str = result[ZimaDB.ATTRIBUTE.DISPLAYSTR].ToString();
            order = Convert.ToInt32(result[ZimaDB.ATTRIBUTE.ORDER].ToString());
        }
    }
}
