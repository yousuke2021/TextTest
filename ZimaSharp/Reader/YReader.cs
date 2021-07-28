using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZimaSharp.Reader
{
    internal abstract class YReader
    {
        protected Error.ErrorList error_list = new();
        public Error.ErrorList Errors { get { return error_list.Clone; } }

        public abstract bool Execution(string text, ref int point);

        public abstract string GetDisplayStr();

        public bool ReadAndMerge(YReader yr, string text, ref int point, Action error_process = null)
        {
            int tmp_point = point;
            if(yr.Execution(text, ref point))
            {
                if (yr.Errors.Exist)
                {
                    error_list.MergeErrors(yr.Errors, tmp_point);
                    error_process?.Invoke();
                }
                return true;
            }
            return false;
        }
    }
}
