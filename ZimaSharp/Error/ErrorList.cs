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

        public bool Exist { get { return (errors.Count > 0); } }


        public ErrorList()
        {

        }

        public void AddError(Error error)
        {
            errors.Add(error);
        }

        public void MergeErrors(ErrorList error_list)
        {
            this.errors.AddRange(error_list.Self);
        }

        public string GetErrorMessage()
        {
            string message = "";
            var sort_errors = errors.OrderBy(x => x.Index);
            foreach(var error in sort_errors)
            {
                message += string.Format("[{0,3}]：{1}\n", error.Index, error.Message);
            }
            return message;
        }
    }
}
