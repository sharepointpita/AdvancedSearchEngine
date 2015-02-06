using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AdvancedSearchTest
{
    class Program
    {
        // Create advanced search for Application Details
        static string conStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

        static void Main(string[] args)
        {
            //ExampleWhereClause();

            ExampleGetDatabaseSchema();
        }

        private static void ExampleGetDatabaseSchema()
        {
            AdvancedSearch.DBSchema.SchemaManager sm = new AdvancedSearch.DBSchema.SchemaManager(conStr);
            var schema = sm.GetSchema();


            // Let the user choose maximal 1 table and related columns to create a query
            for (int i = 0; i < schema.Tables.Count; i++)
            {
                Console.WriteLine("{0}      {1}",i,schema.Tables[i].Name);
            }
            Console.WriteLine("Press the number of the database you want to query:");
            var databaseNr = Convert.ToInt32( Console.ReadLine());

            Console.WriteLine("Press the number columns you want to query comma seperated");
            for (int i = 0; i < schema.Tables[databaseNr].Columns.Count; i++)
            {
                Console.WriteLine("{0}      {1}", i, schema.Tables[databaseNr].Columns[i].Name);
            }
            var commaSepColumns = Console.ReadLine().ToString().Split(new char[] { ',' });


            // Let's create the actual query
            AdvancedSearch.Manager m = new AdvancedSearch.Manager(conStr, schema.Tables[databaseNr].Name);

            StringBuilder sb = new StringBuilder();
            string selectClause = string.Empty;
            sb.AppendLine("SELECT");

            for (int i = 0; i < schema.Tables[databaseNr].Columns.Count; i++)
            {
                if (commaSepColumns.Contains(i.ToString()))
                {
                    sb.AppendFormat("{0},", schema.Tables[databaseNr].Columns[i].Name);

                    m.Fields.Add(new AdvancedSearch.Field(
                        schema.Tables[databaseNr].Columns[i].Name,
                        schema.Tables[databaseNr].Columns[i].Name,
                        schema.Tables[databaseNr].Columns[i].Fieldtype
                        ));
                }
            }

            if (sb.ToString()[sb.ToString().Length - 1] == ',')
            {
               selectClause =  sb.ToString().Remove(sb.ToString().Length - 1);
            }

            m.SetSelectClause(selectClause);
            m.SetFilterType(AdvancedSearch.Enums.FilterType.AND);

            var result = m.CreateQuery();

        }

        private static void ExampleWhereClause()
        {
            AdvancedSearch.Manager m = new AdvancedSearch.Manager(conStr, "App");

            string selectClause = @"SELECT  [App].[Name]
                                           ,[App].[BuildNr]
                                           ,[App].[Version]
                                           ,[App].[Deployed]
                                           ,[App].[DocumentId]";

            //            string fromClause = @"FROM [App]
            //                                  LEFT JOIN [Document] on [App].[DocumentId] = [Document].[Id]";

            m.SetSelectClause(selectClause);
            // m.SetFromClause(fromClause);



            // Create simple FieldTypes (such as Text, Number, Boolean)
            m.Fields.Add(new AdvancedSearch.Field("Application Name", "Name", new AdvancedSearch.TextType()));
            m.Fields.Add(new AdvancedSearch.Field("Build Number", "BuildNr", new AdvancedSearch.NumberType()));
            m.Fields.Add(new AdvancedSearch.Field("Version", "Version", new AdvancedSearch.NumberType()));
            m.Fields.Add(new AdvancedSearch.Field("Is deployed?", "Deployed", new AdvancedSearch.BooleanType()));

            // Create Complex FieldTypes (such as KeyValue (used by dropdownlists))

            string keyValueScript =
            @"SELECT Id, DocumentName
            From Document
            Where Deleted = 0 OR Deleted IS NULL";
            m.Fields.Add(new AdvancedSearch.Field("Document name", "DocumentId", new AdvancedSearch.KeyValueType(keyValueScript)));




            keyValueScript =
            @"SELECT [Id], [Name]
            From KeyUser";
            m.Fields.Add(new AdvancedSearch.Field("Keyuser name", "Name",
                new AdvancedSearch.KeyValueType(keyValueScript),
                "ku",
                "LEFT JOIN [KeyUser] as [ku] on [App].Id = [ku].[AppId]"));




            keyValueScript =
            @"SELECT [Id], [Name]
            From Technology
            WHERE Deleted = 0 OR DELETED IS NULL";
            m.Fields.Add(new AdvancedSearch.Field("Technology", "Name",
                new AdvancedSearch.KeyValueType(keyValueScript),
                "Tech",
                @"left join [App_Technology] on  [App_Technology].AppId = [App].[Id] AND ( [App_Technology].Deleted IS NULL OR [App_Technology].Deleted = 0 )
                  left join [Technology] as [Tech] on [Tech].Id = [App_Technology].TechnologyId"));



            // Initialize Complex FieldTypes. This means complex FieldTypes will get data from databases or executes other logic.
            m.InitializeComplexFields();


            // Set values
            var field = m.Fields.FirstOrDefault(o => o.DisplayName == "Application Name");
            var oper = field.FieldType.DefaultOperator;
            field.SelectedValues.Add(new AdvancedSearch.SelectedValue() { Operator = oper, Value = "test" });


            field = m.Fields.FirstOrDefault(o => o.DisplayName == "Build Number");
            oper = field.FieldType.DefaultOperator;
            field.SelectedValues.Add(new AdvancedSearch.SelectedValue() { Operator = oper, Value = "1" });

            field = m.Fields.FirstOrDefault(o => o.DisplayName == "Version");
            oper = field.FieldType.DefaultOperator;
            field.SelectedValues.Add(new AdvancedSearch.SelectedValue() { Operator = oper, Value = "1.1" });

            field = m.Fields.FirstOrDefault(o => o.DisplayName == "Is deployed?");
            oper = field.FieldType.DefaultOperator;
            field.SelectedValues.Add(new AdvancedSearch.SelectedValue() { Operator = oper, Value = "true" });


            field = m.Fields.FirstOrDefault(o => o.DisplayName == "Keyuser name");
            oper = field.FieldType.DefaultOperator;
            field.SelectedValues.Add(new AdvancedSearch.SelectedValue() { Operator = oper, Value = "peter" });

            field = m.Fields.FirstOrDefault(o => o.DisplayName == "Document name");
            oper = field.FieldType.DefaultOperator;
            field.SelectedValues.Add(new AdvancedSearch.SelectedValue() { Operator = oper, Value = "2" });

            field = m.Fields.FirstOrDefault(o => o.DisplayName == "Technology");
            oper = field.FieldType.DefaultOperator;
            field.SelectedValues.Add(new AdvancedSearch.SelectedValue() { Operator = oper, Value = "IIS" });

            m.SetFilterType(AdvancedSearch.Enums.FilterType.AND);

            var result = m.CreateQuery();
        }
    }





}
