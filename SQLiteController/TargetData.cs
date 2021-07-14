using System.Collections.Generic;

namespace SQLiteController
{
    public class TargetData
    {
        private Table table;
        private List<string> target_columns = new();

        private Dictionary<string, string> equal_condition = new();
        private Dictionary<string, string> nequal_condition = new();

        private Dictionary<string, string> less_condition = new();
        private Dictionary<string, string> greater_condition = new();
        private Dictionary<string, string> lequal_condition = new();
        private Dictionary<string, string> gequal_condition = new();

        private string condition = "";

        public Table Table { get { return table; } }

        public List<string> TargetColumns { get { return target_columns; } }

        public Dictionary<string, string> EqualCondition { get { return new Dictionary<string, string>(equal_condition); } }
        public Dictionary<string, string> NEqualCondition { get { return new Dictionary<string, string>(nequal_condition); } }
        public Dictionary<string, string> LessCondition { get { return new Dictionary<string, string>(less_condition); } }
        public Dictionary<string, string> GeaterCondition { get { return new Dictionary<string, string>(greater_condition); } }
        public Dictionary<string, string> LEqualCondition { get { return new Dictionary<string, string>(lequal_condition); } }
        public Dictionary<string, string> GEqualCondition { get { return new Dictionary<string, string>(gequal_condition); } }

        public string Condition { get { return condition; } }

        public TargetData(Table table)
        {
            this.table = table;
        }

        public TargetData(Table table, params string[] target_columns)
        {
            this.table = table;
            SetTargetColumns(target_columns);
        }

        public TargetData SetTargetColumns(params string[] target_columns)
        {
            this.target_columns = new();
            foreach(var target_column in target_columns)
            {
                this.target_columns.Add(table.Columns[target_column]);
            }
            return this;
        }

        public TargetData SetTargetAll()
        {
            this.target_columns = new(table.Columns.Values);
            return this;
        }

        public TargetData SetCondition(string condition)
        {
            this.condition = condition;
            return this;
        }

        public TargetData SetEqualCondition(params (string columns, string value)[] conditions)
        {
            equal_condition = new();
            foreach(var condition in conditions)
            {
                equal_condition.Add(condition.columns, condition.value);
            }
            return this;
        }

        public TargetData SetNEqualCondition(params (string columns, string value)[] conditions)
        {
            nequal_condition = new();
            foreach (var condition in conditions)
            {
                nequal_condition.Add(condition.columns, condition.value);
            }
            return this;
        }

        public TargetData SetLessCondition(params (string columns, string value)[] conditions)
        {
            less_condition = new();
            foreach (var condition in conditions)
            {
                less_condition.Add(condition.columns, condition.value);
            }
            return this;
        }

        public TargetData SetGreaterCondition(params (string columns, string value)[] conditions)
        {
            greater_condition = new();
            foreach (var condition in conditions)
            {
                greater_condition.Add(condition.columns, condition.value);
            }
            return this;
        }

        public TargetData SetLEqualCondition(params (string columns, string value)[] conditions)
        {
            lequal_condition = new();
            foreach (var condition in conditions)
            {
                lequal_condition.Add(condition.columns, condition.value);
            }
            return this;
        }

        public TargetData SetGEqualCondition(params (string columns, string value)[] conditions)
        {
            gequal_condition = new();
            foreach (var condition in conditions)
            {
                gequal_condition.Add(condition.columns, condition.value);
            }
            return this;
        }

    }
}
