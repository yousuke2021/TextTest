using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Error
{
    internal class ErrorList
    {
        private List<Error> errors = new();

        public List<Error> Self { get { return new List<Error>(errors); } }

        public ErrorList Clone { 
            get { ErrorList clone = new ErrorList();
                clone.MergeErrors(this); 
                return clone; } 
        }

        public bool Exist { get { return (errors.Count > 0); } }


        public ErrorList()
        {

        }

        public void AddError(Error error)
        {
            errors.Add(error);
        }

        public void AddError(Error error, int index)
        {
            errors.Add(error.SetIndex(index));
        }

        public void MergeErrors(ErrorList error_list)
        {
            errors.AddRange(error_list.Self);
        }

        public void MergeErrors(ErrorList error_list, int diff)
        {
            foreach(var error in error_list.Self)
            {
                errors.Add(new Error(error.Index + diff, error.Message));
            }
        }

        public string GetErrorMessage()
        {
            string message = "";

            foreach(var error in errors.OrderBy(x => x.Index))
            {
                message += string.Format("[{0,3}]：{1}\n", error.Index, error.Message);
            }
            return message;
        }
    }
}
