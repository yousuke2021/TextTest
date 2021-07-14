using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Assets
{
    public class Verb
    {
        int id;
        string name;
        string display_str;
        Type subject_type;
        Type object_type;
        Type complement_type;

        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public string DisplayStr { get { return display_str; } }
        public Type Subject { get { return subject_type; } }
        public Type Object { get { return object_type; } }
        public Type Complement { get { return complement_type; } }

        public Verb(int id, string name, string display_str, Type subject_type, Type object_type, Type complement_type)
        {
            this.id = id;
            this.name = name;
            this.display_str = display_str;
            this.subject_type = subject_type;
            this.object_type = object_type;
            this.complement_type = complement_type;
        }

        public Verb(SQLiteDataReader result)
        {
            this.id = Convert.ToInt32(result[ZimaDB.VERB.ID].ToString());
            this.name = result[ZimaDB.VERB.NAME].ToString();
            this.display_str = result[ZimaDB.VERB.DISPLAYSTR].ToString();
        }

        public void SetSubject(Type type)
        {
            subject_type = type;
        }

        public void SetObject(Type type)
        {
            object_type = type;
        }

        public void SetComplement(Type type)
        {
            complement_type = type;
        }

    }
}
