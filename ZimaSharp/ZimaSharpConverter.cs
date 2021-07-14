using SQLiteController;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimaSharp.Reader;

namespace ZimaSharp
{
    public class ZimaSharpConverter
    {
        private static readonly List<Assets.Type> types = new();
        private static readonly List<Assets.Verb> verbs = new();

        private static string zimapath;
        private static string funcpath;
        

        private static bool init = false;
        private static ZimaDB zimadb;

        public static List<Assets.Type> Types { get { return new List<Assets.Type>(types); } }
        public static List<Assets.Verb> Verbs { get { return new List<Assets.Verb>(verbs); } }
        
        public static  string ZimaPath { get { return zimapath; } }
        public static string FuncPath { get { return funcpath; } }

        public ZimaSharpConverter()
        {
            
        }

        public static void Init(string path1, string path2)
        {
            init = true;
            zimapath = path1;
            funcpath = path2;
            InitZimaDB();

        }

        private static void InitZimaDB()
        {
            zimadb = new ZimaDB(zimapath);
            InitType();
            InitNoun();
            InitAttribute();
            InitArgument();
            InitVerb();
            //OutPutStatus();
        }

        public static void OutPutStatus()
        {
            Debug.WriteLine(string.Format("types:{0}-----", types.Count));
            foreach(var type in types)
            {
                Debug.WriteLine(string.Format("name:{0}", type.Name));
                Debug.Write("nouns:\n");
                foreach(var noun in type.Nouns)
                {
                    Debug.Write(string.Format("  key:{0} display_str:{1}\n", noun.Key, noun.DisplayStr));
                }
                Debug.Write("\n");
                Debug.Write("attributes:");
                foreach(var attribute in type.Attributes)
                {
                    Debug.Write(attribute.Name + " ");
                }
                Debug.Write("\n");
                Debug.Write("arguments:");
                foreach(var argument in type.Arguments)
                {
                    Debug.Write(argument.Name + " ");
                }
                Debug.WriteLine("\n");
            }
        }

        private static void ProcessToGetData(Table table, Action<SQLiteDataReader> action)
        {
            TargetData td = new (table);
            td.SetTargetAll();
            SQL.SelectCommand command = SQL.SelectData(td);
            SQCon sqc = new(zimadb);
            sqc.Open();
            foreach (var result in sqc.GetData(command.Self))
            {
                action(result);
            }
            sqc.Close();
        }

        private static void InitType()
        {
            types.Clear();
            Action<SQLiteDataReader> action = (result) => {
                types.Add(new Assets.Type(result));
            };
            ProcessToGetData(zimadb.TypeTable, action);
        }

        private static void InitNoun()
        {
            Action<SQLiteDataReader> action = (result) =>
            {
                Assets.Noun noun = new(result);
                for (int i = 0; i < types.Count; i++)
                {
                    if (noun.TypeId == types[i].Id)
                    {
                        types[i].AddNoun(noun);
                        break;
                    }
                }
            };
            ProcessToGetData(zimadb.NounTable, action);
        }

        private static void InitAttribute()
        {
            Action<SQLiteDataReader> action = (result) =>
            {
                Assets.Attribute attribute = new(result);
                for (int i = 0; i < types.Count; i++)
                {
                    if (attribute.TypeId == types[i].Id)
                    {
                        types[i].AddAttribute(attribute);
                        break;
                    }
                }
            };
            ProcessToGetData(zimadb.AttributeTable, action);
        }

        private static void InitArgument()
        {
            Action<SQLiteDataReader> action = (result) =>
            {
                int type_id = Convert.ToInt32(result[ZimaDB.ARGUMENT.TYPEID].ToString());
                int argument_id = Convert.ToInt32(result[ZimaDB.ARGUMENT.ARGUMENTID].ToString());

                Assets.Type argument = types.Where(x => x.Id == argument_id).FirstOrDefault();
                if(argument == null)
                {
                    return;
                }
                for (int i = 0; i < types.Count; i++)
                {
                    if (type_id == types[i].Id)
                    {
                        types[i].AddArgument(argument);
                        break;
                    }
                }
            };
            ProcessToGetData(zimadb.ArgumentTable, action);
        }

        private static void InitVerb()
        {
            Action<SQLiteDataReader> action = (result) =>
            {
                int subject_id = TextReader.SToUI(result[ZimaDB.VERB.SUBJECTID].ToString());
                int object_id = TextReader.SToUI(result[ZimaDB.VERB.OBJECTID].ToString());
                int complement_id = TextReader.SToUI(result[ZimaDB.VERB.COMPLEMENTID].ToString());

                Assets.Verb verb = new(result);

                Assets.Type _subject = types.Where(x => x.Id == subject_id).FirstOrDefault();
                if (_subject != null)
                {
                    verb.SetSubject(_subject);
                }

                Assets.Type _object = types.Where(x => x.Id == object_id).FirstOrDefault();
                if (_object != null)
                {
                    verb.SetObject(_object);
                }

                Assets.Type _complement = types.Where(x => x.Id == complement_id).FirstOrDefault();
                if (_complement != null)
                {
                    verb.SetComplement(_complement);
                }
                verbs.Add(verb);
            };
            ProcessToGetData(zimadb.VerbTable, action);
        }

        public static string HelloWorld()
        {
            return "Hello ZimaSharp!!";
        }

        public static string HelloSQLite()
        {
            return SQLiteController.SQCon.HelloWorld();
        }

        public bool NounReaderTest(string text, int point)
        {
            Reader.NounWrapperReader nwr = new();
            return nwr.Execution(text, ref point);
        }


    }
}
