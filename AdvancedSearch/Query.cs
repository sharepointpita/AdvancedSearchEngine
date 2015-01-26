using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedSearch
{
    public class Query
    {
        private string _selectClause;
        public string SelectClause
        {
            get { return _selectClause; }
            set { _selectClause = value; }
        }
        
        private string _fromClause;
        public string FromClause
        {
            get { return _fromClause; }
            set { _fromClause = value; }
        }
        
        private string _whereClause;
        public string WhereClause
        {
            get { return _whereClause; }
            set { _whereClause = value; }
        }

        private string _rootTable;

       // private List<Field> _fields;
       // public List<Field> Fields { get { return _fields; } set { _fields = value; } }

        public Enums.FilterType FilterType { get; set; }

        public Query(string rootTable, Enums.FilterType filterType)
        {
            _rootTable = rootTable;
            FilterType = filterType;
            FilterType = Enums.FilterType.OR;
        }

        public string CreateQuery(List<Field> fields)
        {
            if (string.IsNullOrWhiteSpace(_rootTable) || fields.Count == 0)
                throw new Exception("SQL cannot be created while the rootTable is not defined or either there're no Fields defined.");

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(_selectClause);

            _fromClause = string.Empty;
            _whereClause = string.Empty;

            _fromClause = (string.Format("FROM [{0}]{1}", _rootTable,Environment.NewLine));
           _whereClause = ConstructWhere(fields);

            sb.AppendLine(_fromClause);
            sb.AppendLine(_whereClause);

            return sb.ToString();
        }

        #region Query

        public string ConstructFrom()
        {
            throw new NotImplementedException();
        }

        public string ConstructWhere(List<Field> fields)
        {
            StringBuilder sb = new StringBuilder();
            

            Field f;
            string table;
            bool whereInserted = false;

            for (int i = 0; i < fields.Count; i++)
            {
                f = fields[i];

                if (f.SelectedValues.Count > 0)
                {

                    if (!whereInserted)
                    {
                        sb.AppendLine("WHERE ");
                        whereInserted = true;
                    }

                    table = !string.IsNullOrWhiteSpace(f.SourceTable) ? f.SourceTable : _rootTable;

                    if (f.UseCustomJoin)
                        _fromClause += string.Format(f.CustomJoin + "{0}", Environment.NewLine);

                    for (int j = 0; j < f.SelectedValues.Count; j++)
                    {
                        sb.Append(string.Format("[{0}].[{1}] {2} {3} ",
                            table,
                            f.SqlFieldName,
                            string.Format(f.SelectedValues[j].Operator.SqlSyntax, f.SelectedValues[j].Value),
                            Environment.NewLine + FilterType.ToString()));
                    }
                }

            }

            if (sb.Length - FilterType.ToString().Length - 1 > -1)
            {
                sb = sb.Remove(sb.Length - FilterType.ToString().Length - 1, FilterType.ToString().Length);
            }

            sb.AppendLine();

            return sb.ToString();
        }

        #endregion

    }
}
