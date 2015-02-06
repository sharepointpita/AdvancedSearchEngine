using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Enums = AdvancedSearch.Enums;
using System.Data;
using System.Data.SqlClient;

namespace AdvancedSearch
{
    /// <summary>
    /// Class which contains the logic to manage search fields
    /// </summary>
    public class Manager
    {
        private static string _connectionString;
        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        private Query _query;

        private List<Field> _fields;
        public List<Field> Fields { get { return _fields; } set { _fields = value; } }

        #region Constructors

        public Manager(string connectionString, string rootTable)
        {
            _connectionString = connectionString;

            Initialize(rootTable,null, Enums.FilterType.OR);
        }

        public Manager(string connectionString,  string rootTable, List<Field> fields)
            : this(connectionString,rootTable)
        {
            Initialize(rootTable,fields, Enums.FilterType.OR);
        }

        public Manager(string connectionString,  string rootTable, List<Field> fields, Enums.FilterType filterType)
            : this(connectionString, rootTable)
        {
            Initialize(rootTable,fields, filterType);
        }

        private void Initialize(string rootTable, List<Field> fields, Enums.FilterType filterType)
        {
            if (fields == null)
                _fields = new List<Field>();
            else
                _fields = fields;

            _query = new Query(rootTable.ToLower(), filterType);
        }

        #endregion


        public void SetSelectClause(string clause)
        {
            _query.SelectClause = clause;
        }

        public void SetFromClause(string clause)
        {
            _query.FromClause = clause;
        }

        public void SetWhereClause(string clause)
        {
            _query.WhereClause = clause;
        }

        public void ClearSelectedValues()
        {
            foreach (var field in _fields)
            {
                field.SelectedValues.Clear();
            }
        }

        public void SetFilterType(Enums.FilterType filterType)
        {
            _query.FilterType = filterType;
        }

        /// <summary>
        /// Lazy Load / Initialize complex fields
        /// </summary>
        public void InitializeComplexFields()
        {
            var fields = _fields.Where(o => o.FieldType.GetType().BaseType == typeof( ComplexTypeBase));

            FieldTypeBase baseT;
            ComplexTypeBase comT;
            foreach (var field in fields)
            {
                baseT = field.FieldType;
                comT = (ComplexTypeBase)field.FieldType;

                comT.InitializeAdditionalMembers();
            }
        }


        public string CreateQuery()
        {
            return _query.CreateQuery(Fields);
        }

        public string CreateQuery(Enums.FilterType filterType)
        {
            _query.FilterType = filterType;
            return _query.CreateQuery(Fields);
        }

        public DataSet ExecuteQuery(string sqlScript)
        {
            DataSet ds = null;

            string conStr = Manager.ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                con.Open();

                using (SqlDataAdapter adap = new SqlDataAdapter(sqlScript, con))
                {
                    ds = new DataSet();
                    adap.Fill(ds);
                }
            }

            return ds;
        }
        
    }
}
