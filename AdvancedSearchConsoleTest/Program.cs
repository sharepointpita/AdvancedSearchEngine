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
            AdvancedSearch.Manager m = new AdvancedSearch.Manager(conStr, "App");
            m.GetDatabaseSchema();
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
            m.Fields.Add(new AdvancedSearch.Field("Build Number", "BuildNr", new AdvancedSearch.Number()));
            m.Fields.Add(new AdvancedSearch.Field("Version", "Version", new AdvancedSearch.Number()));
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
