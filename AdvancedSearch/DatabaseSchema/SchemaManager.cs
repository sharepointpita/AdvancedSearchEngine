//https://msdn.microsoft.com/en-us/library/ms254969(v=vs.110).aspx

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AdvancedSearch.DBSchema.DBObjects;

namespace AdvancedSearch.DBSchema
{


    public class SchemaManager
    {
        private static string _connectionString;
        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        #region Constructors

        public SchemaManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        public AdvancedSearch.DBSchema.DBObjects.Database GetSchema()
        {
            Database schema = new Database();

            DataTable tableSchema = null;
            DataTable columnSchema = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {

                con.Open();

                // Get all Tables.
                tableSchema = con.GetSchema("Tables");
                ConstructTables(ref schema,tableSchema);

                // Get all Columns
                columnSchema = con.GetSchema("Columns");
                ConstructColumns(ref schema,columnSchema);
            }

            // Filter out Columns which has no implemented FieldType.
            ExcludeNotImplementedFieldTypes(ref schema);
                
            return schema;

        }

        private void ConstructTables(ref Database schema,  DataTable tableSchema)
        {
            foreach (DataRow row in tableSchema.Rows)
            {
                schema.Tables.Add(
                    new Table()
                    {
                        Name = row[Table.Table_Name].ToString(),
                        Type = row[Table.Table_Type].ToString(),
                        Schema = row[Table.table_schema].ToString()
                    });
            }
        }

        private void ConstructColumns(ref Database schema, DataTable columnSchema)
        {
            foreach (DataRow row in columnSchema.Rows)
            {
                var table = schema.Tables.FirstOrDefault(o => o.Name == row[Column.column_tableName].ToString());

                if (table != null)
                {
                    table.Columns.Add(
                        new Column()
                        {
                            Name = row[Column.column_Name].ToString(),
                            Position = Convert.ToInt16(row[Column.column_position]),
                            DataType = row[Column.column_data_type].ToString()
                        });
                }

            }
        }

        private void ExcludeNotImplementedFieldTypes(ref Database schema)
        {
            foreach (Table t in schema.Tables)
            {
                t.Columns.RemoveAll(p => p.Fieldtype == null);
            }
        }
    }
}
