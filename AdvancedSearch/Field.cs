using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdvancedSearch
{
    public class Field
    {
        private string _displayName;

        /// <summary>
        /// DisplayName is used to display text on the screen and is also used as UniqueIdentifier when used within a collection
        /// </summary>
        public string DisplayName { get { return _displayName; } }

        private string _sqlFieldName;
        public string SqlFieldName { get { return _sqlFieldName; } }

        private FieldTypeBase _fieldType;
        public FieldTypeBase FieldType { get { return _fieldType; } }

        private string _sourceTable;
        public string SourceTable 
        { 
            get 
            { 
                // Ensure that sourceTable is enclosed by '[]'
                if (!string.IsNullOrWhiteSpace(_sourceTable))
                {
                    if (!_sourceTable.StartsWith("["))
                        _sourceTable.Insert(0, "[");

                    if (!_sourceTable.EndsWith("]"))
                        _sourceTable.Insert(_sourceTable.Length, "[");
                }
                return _sourceTable; 
            } 
        }

        private string _foreingKey;
        public string ForeignKey { get { return _foreingKey; } }

        private string _lookupKey;
        public string LookupKey { get { return _lookupKey; } }


        public List<SelectedValue> SelectedValues { get; set; }


        private string _customJoin;
        public string CustomJoin { get { return _customJoin; } set { _customJoin = value; } }

        internal Boolean UseCustomJoin
        {
            get 
            {
                return _customJoin != null;
            }
        }


        public Field()
         {

         }

        public Field(string displayName, string sqlFieldName, FieldTypeBase fieldType,
            string sourceTable, string customJoin)
        {
            SelectedValues = new List<SelectedValue>();

            _displayName = displayName;
            _sqlFieldName = sqlFieldName.ToLower();
            _fieldType = fieldType;
            _sourceTable = !string.IsNullOrWhiteSpace(sourceTable) ? sourceTable.ToLower() : null;
            _customJoin = !string.IsNullOrWhiteSpace(customJoin) ? customJoin.ToLower() : null;
        }

        public Field(string displayName, string sqlFieldName, FieldTypeBase fieldType,
            string sourceTable = null, string foreingKey = null, string lookupKey = null) : this(displayName,sqlFieldName,fieldType,sourceTable,null)
        {
            _foreingKey = !string.IsNullOrWhiteSpace(foreingKey) ? foreingKey.ToLower() : null;
            _lookupKey = !string.IsNullOrWhiteSpace(lookupKey) ? lookupKey.ToLower() : null;
        }

        public void SetCustomJoin(string join)
        {
            _customJoin = join;
        }

        public override string ToString()
        {
            return _displayName;
        }

    }
}
