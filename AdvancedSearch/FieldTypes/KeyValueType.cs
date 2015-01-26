using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace AdvancedSearch
{
    public class KeyValueType : ComplexTypeBase
    {

        public List<KeyValuePair<string,string>> KeyValues {get;set;}
        private string _sqlScript;

        /// <summary>
        /// sqlScript should contain a select statent with 2 columns. First column must be the key and second column value.
        /// The class self will then handle the logic to fill the 'KeyValues' property
        /// </summary>
        /// <param name="sqlScript"></param>
        public KeyValueType(string sqlScript)
            : base(Enums.FieldTypeName.KeyValue)
        {
            KeyValues = new List<KeyValuePair<string, string>>();
            _sqlScript = sqlScript;
        }


        public override List<Operator> Operators
        {
            get
            {
                return new List<Operator>()
                {
                    new Operator("=", "= '{0}'")
                };
            }
        }

        public override Operator DefaultOperator
        {
            get
            {
                return base.DefaultOperator;
            }
        }

        public override void InitializeAdditionalMembers()
        {
            GetKeyValuesFromDatabase();
        }

        #region Custom logic For type specific

        private void GetKeyValuesFromDatabase()
        {
            string conStr = Manager.ConnectionString;

            using (SqlConnection con = new SqlConnection(conStr))
            {
                using (SqlCommand com = new SqlCommand(_sqlScript, con))
                {
                    con.Open();

                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            KeyValues.Add(new KeyValuePair<string, string>(reader[0].ToString(), reader[1].ToString()));
                        }
                    }
                }
            }
        }

        #endregion

    }
}