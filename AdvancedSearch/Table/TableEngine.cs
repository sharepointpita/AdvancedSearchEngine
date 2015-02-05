//https://msdn.microsoft.com/en-us/library/ms254969(v=vs.110).aspx

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AdvancedSearch.Table
{


    public static class TableManager
    {

        

        public static DataTable GetSchema()
        {
            string conStr = Manager.ConnectionString;
            DatabaseSchema schema = new DatabaseSchema();

            DataTable tableSchema = null;
            DataTable columnSchema = null;

            using (SqlConnection con = new SqlConnection(conStr))
            {

                con.Open();

                // Get all Tables.
                tableSchema = con.GetSchema("Tables");
                ConstructTables(ref schema,tableSchema);

                // Get all Columns
                columnSchema = con.GetSchema("Columns");
                ConstructColumns(ref schema,columnSchema);
            }

            return tableSchema;

        }

        private static void ConstructTables(ref DatabaseSchema schema,  DataTable tableSchema)
        {
            foreach (DataRow row in tableSchema.Rows)
            {
                schema.Tables.Add(
                    new Table()
                    {
                        Name = row[Constants.Table_Name].ToString()
                    });
            }
        }

        private static void ConstructColumns(ref DatabaseSchema schema, DataTable columnSchema)
        {
            foreach (DataRow row in columnSchema.Rows)
            {
                var table = schema.Tables.FirstOrDefault(o => o.Name == row[Constants.column_tableName].ToString());

                if (table != null)
                {
                    table.Columns.Add(
                        new Column()
                        {
                            Name = row[Constants.column_Name].ToString()
                        });
                }

            }
        }

    }
}
