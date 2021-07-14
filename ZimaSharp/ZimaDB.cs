using SQLiteController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp
{
    internal class ZimaDB : DB
    {

        public class TYPE
        {
            public static readonly string SELF = "type";
            public static readonly string ID = "id";
            public static readonly string NAME = "name";
            public static readonly string ORDER = "ord";
            public static readonly string PARENT = "parent";
            public static readonly string BASE = "base";
            public static readonly string CANUSE = "canuse";
            public static readonly string UNCOUNTABLE = "uncountable";
            public static readonly string KEYDB = "key_db";
            public static readonly string KEYTABLE = "key_table";
            public static readonly string KEYCOLUMN = "key_column";
            public static readonly string DISPLAYTABLE = "display_table";
            public static readonly string DISPLAYCOLUMN = "display_column";
            public static readonly string TEMPLATE = "template";
        }

        public class NOUN
        {
            public static readonly string SELF = "noun";
            public static readonly string TYPEID = "type_id";
            public static readonly string KEY = "key";
            public static readonly string DISPLAYSTR = "display_str";
        }

        public class ARGUMENT
        {
            public static readonly string SELF = "argument";
            public static readonly string TYPEID = "type_id";
            public static readonly string ARGUMENTID = "argument_id";
        }

        public class ATTRIBUTE
        {
            public static readonly string SELF = "attribute";
            public static readonly string TYPEID = "type_id";
            public static readonly string NAME = "name";
            public static readonly string ATTRIBUTETYPE = "attribute_type";
            public static readonly string DISPLAYSTR = "display_str";
            public static readonly string ORDER = "ord";
        }

        public class VERB
        {
            public static readonly string SELF = "verb";
            public static readonly string ID = "id";
            public static readonly string NAME = "name";
            public static readonly string DISPLAYSTR = "display_str";
            public static readonly string SUBJECTID = "subject_id";
            public static readonly string OBJECTID = "object_id";
            public static readonly string COMPLEMENTID = "complement_id";
        }

        private Table type_table;
        private Table noun_table;
        private Table argument_table;
        private Table attribute_table;
        private Table verb_table;
        
        public Table TypeTable { get { return type_table; } }
        public Table NounTable { get { return noun_table; } }
        public Table ArgumentTable { get { return argument_table; } }
        public Table AttributeTable { get { return attribute_table; } }
        public Table VerbTable { get { return verb_table; } }

        public ZimaDB(string path) : base(path)
        {
            InitTable();
        }

        public ZimaDB(string path, params Table[] tables) : this(path)
        {

        }

        public ZimaDB(string path, params (string key, Table value)[] tables) : this(path)
        {

        }

        private void InitTable()
        {
            tables = new();

            type_table = new Table(TYPE.SELF, TYPE.ID, TYPE.NAME, TYPE.ORDER, TYPE.PARENT, TYPE.BASE, TYPE.CANUSE,
                TYPE.UNCOUNTABLE, TYPE.KEYDB, TYPE.KEYTABLE, TYPE.KEYCOLUMN, TYPE.DISPLAYTABLE, TYPE.DISPLAYCOLUMN, TYPE.TEMPLATE);
            AddTable(type_table);

            noun_table = new Table(NOUN.SELF, NOUN.TYPEID, NOUN.KEY, NOUN.DISPLAYSTR);
            AddTable(noun_table);

            argument_table = new Table(ARGUMENT.SELF, ARGUMENT.TYPEID, ARGUMENT.ARGUMENTID);
            AddTable(argument_table);

            attribute_table = new Table(ATTRIBUTE.SELF, ATTRIBUTE.TYPEID, ATTRIBUTE.NAME,
                ATTRIBUTE.ATTRIBUTETYPE, ATTRIBUTE.DISPLAYSTR, ATTRIBUTE.ORDER);
            AddTable(attribute_table);

            verb_table = new Table(VERB.SELF, VERB.ID, VERB.NAME, VERB.DISPLAYSTR, VERB.SUBJECTID, VERB.OBJECTID, VERB.COMPLEMENTID);
            AddTable(verb_table);
        }


    }
}
