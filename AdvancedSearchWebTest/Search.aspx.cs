using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace AdvancedSearchWebTest
{
    public partial class Search : System.Web.UI.Page
    {
        public List<SelectedSearchField> SelectedSearchFields
        {
            get
            {
                return (List<SelectedSearchField>)Session["SelectedSearchFields"];
            }
            set
            {
                Session["SelectedSearchFields"] = value;
            }
        }

        public AdvancedSearch.Manager Manager
        {
            get
            {
                return (AdvancedSearch.Manager)Session["manager"];
            }
            set
            {
                Session["manager"] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            dlSearchCriteria.ItemDataBound += new DataListItemEventHandler(dlSearchCriteria_ItemDataBound);
            dlSearchCriteria.ItemCommand += new DataListCommandEventHandler(dlSearchCriteria_ItemCommand);

        }

        void dlSearchCriteria_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlFields");

                foreach (var field in Manager.Fields.OrderBy(o => o.DisplayName))
                {
                    ddl.Items.Add(new ListItem(field.DisplayName, field.DisplayName));
                }

                ddl.Items.Insert(0, "");
            }
            else if (e.Item.ItemType == ListItemType.Item)
            {
                // Hide dummy row
                SelectedSearchField dmy = (SelectedSearchField)e.Item.DataItem;
                if (dmy.fieldDisplayName == "")
                {
                    var x = e.Item.FindControl("trItemTemplate");
                    x.Visible = false;
                }

            }
        }

        void dlSearchCriteria_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("addNewCriteria"))
            {
                AddNewCriteria(e);
            }
            else if (e.CommandName.Equals("DeleteSearchCriteria"))
            {
                DeleteCriteria(e);
            }
        }

        private void AddNewCriteria(DataListCommandEventArgs e)
        {
            DropDownList ddlField = (DropDownList)e.Item.FindControl("ddlFields");
            var field = Manager.Fields.First(o => o.DisplayName == ddlField.SelectedValue);

            Control ddlOperator = e.Item.FindControl("ddlOperators");

            string oper = string.Empty;
            string val = string.Empty;
            string userFriendlyVal = string.Empty;

            if (ddlOperator != null)
            {
                oper = (ddlOperator as DropDownList).SelectedValue;
            }

            var key = Request.Params.AllKeys.FirstOrDefault(o => o.Contains("CtrlValue"));

            if (!string.IsNullOrWhiteSpace(key))
            {
                val = Request.Params[key];

                if (field.FieldType.Type == AdvancedSearch.Enums.FieldTypeName.KeyValue)
                {
                    userFriendlyVal = (field.FieldType as AdvancedSearch.KeyValueType).KeyValues.First(o => o.Key == val).Value;
                }
                else
                {
                    userFriendlyVal = val;
                }

            }
            else if (field.FieldType.Type != AdvancedSearch.Enums.FieldTypeName.Boolean)
            {
                // show error
            }



            SelectedSearchFields.Add(new SelectedSearchField()
            {
                fieldDisplayName = field.DisplayName,
                operatorDisplayName = oper,
                value = val,
                UserFriendlyValue = userFriendlyVal
            });

            dlSearchCriteria.DataSource = SelectedSearchFields;
            dlSearchCriteria.DataBind();
        }

        private void DeleteCriteria(DataListCommandEventArgs e)
        {
            var id = ((Label) e.Item.FindControl("lblId")).Text;

            var selectedItem = SelectedSearchFields.FirstOrDefault(o => o.Id == new Guid(id));

            SelectedSearchFields.Remove(selectedItem); 
            dlSearchCriteria.DataSource = SelectedSearchFields;
            dlSearchCriteria.DataBind();
        }

        public void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlListFields = (DropDownList)sender;
            var footer = ddlListFields.NamingContainer;
            var ddlListOperators = (DropDownList)footer.FindControl("ddlOperators");
            var pnl = (Panel)footer.FindControl("pnlvalueContainer");

            ddlListOperators.Items.Clear();

            if (!string.IsNullOrWhiteSpace(ddlListFields.SelectedValue))
            {
                var selectedField = Manager.Fields.FirstOrDefault(o => o.DisplayName == ddlListFields.SelectedValue);

                foreach (var op in selectedField.FieldType.Operators)
                {
                    ddlListOperators.Items.Add(new ListItem(op.DisplayName, op.DisplayName));
                }

                // Add control to literal value
               

                ConstuctValueControl(pnl, selectedField);
            }
            else
            {
                pnl.Controls.Clear();
            }
            
        }

        private void ConstuctValueControl(Panel valueContainer, AdvancedSearch.Field field)
        {
            valueContainer.Controls.Clear();

            switch (field.FieldType.Type)
            {
                case AdvancedSearch.Enums.FieldTypeName.Text:

                    TextBox tb = new TextBox();
                    tb.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    tb.ID = "CtrlValue";
                    valueContainer.Controls.Add(tb);
                    tb.EnableViewState = false; 
                    break;

                case AdvancedSearch.Enums.FieldTypeName.Boolean:

                    //CheckBox chk = new CheckBox();
                    //chk.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    //chk.ID = "CtrlValue";
                    //valueContainer.Controls.Add(chk);
                    //chk.EnableViewState = false; 
                    break;

                case AdvancedSearch.Enums.FieldTypeName.Number:

                    TextBox tbNr = new TextBox();
                    tbNr.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    tbNr.ID = "CtrlValue";
                    valueContainer.Controls.Add(tbNr);
                    break;

                case AdvancedSearch.Enums.FieldTypeName.KeyValue:

                    DropDownList ddl = new DropDownList();
                    ddl.ClientIDMode = System.Web.UI.ClientIDMode.Static;
                    ddl.ID = "CtrlValue";
                    valueContainer.Controls.Add(ddl);
                    SetDropDownListValues(ddl, (AdvancedSearch.KeyValueType)field.FieldType);
                    ddl.EnableViewState = false;
                    break;
            }
        }

        public void SetDropDownListValues(DropDownList ddl, AdvancedSearch.KeyValueType type)
        {
            ddl.Items.Clear();

            foreach (var kvp in type.KeyValues)
            {
                ddl.Items.Add(new ListItem(kvp.Value, kvp.Key));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Manager has not been created by the Select.aspx
                if (Manager == null)
                {
                    SetSearchFields();
                }


                // Add dummy item 
                List<SelectedSearchField> dmy = new List<SelectedSearchField>();
                dmy.Add(new SelectedSearchField() { fieldDisplayName = "", operatorDisplayName = "", value = "" });


                SelectedSearchFields = dmy;

                //dlSearchCriteria.DataSourceID = "fieldDisplayName";
                dlSearchCriteria.DataSource = SelectedSearchFields;
                dlSearchCriteria.DataBind();

            }
            else
            {
                
            }
        }

        private void SetSearchFields()
        {
            // Create advanced search for Application Details
            string conStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

            Manager = new AdvancedSearch.Manager(conStr, "App");
            AdvancedSearch.Manager m = Manager;

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


           
        }

        private void SetValues()
        {
            AdvancedSearch.Manager m = Manager;

            m.ClearSelectedValues();

            AdvancedSearch.Field field;
            foreach (var criteria in SelectedSearchFields.Where(o => o.fieldDisplayName != "")) // Eliminate dummy criteria needed by datalist
            {
                field = m.Fields.FirstOrDefault(o => o.DisplayName == criteria.fieldDisplayName);

                var newValue = new AdvancedSearch.SelectedValue();

                newValue.Operator = field.FieldType.Operators.FirstOrDefault(o => o.DisplayName == criteria.operatorDisplayName);

                if (field.FieldType.Type == AdvancedSearch.Enums.FieldTypeName.KeyValue)
                {
                    newValue.Value = criteria.UserFriendlyValue;
                }
                else
                    newValue.Value = criteria.value;

                field.SelectedValues.Add(newValue);
            }

            if (rblAndOr.SelectedValue == "AND")
                Manager.SetFilterType(AdvancedSearch.Enums.FilterType.AND);
            else
                Manager.SetFilterType(AdvancedSearch.Enums.FilterType.OR);

      
        }

        public void btnDoSearch_OnClick(object sender, EventArgs e)
        {
            SetValues();
            var sqlScript =  Manager.CreateQuery();
            txtQuery.Text = sqlScript;

            var table = Manager.ExecuteQuery(sqlScript).Tables[0];
            grdDynamic.DataSource = table;
            grdDynamic.DataBind();

            lblNoDataSourceResult.Visible = (table == null ||  table.Rows.Count == 0);
        }

    }

    public class SelectedSearchField
    {
        private Guid _Id;
        public Guid Id { get { return _Id; } }
        public string fieldDisplayName {get;set;}
        public string operatorDisplayName { get; set; }
        public string value { get; set; }
        public string UserFriendlyValue { get; set; }

        public SelectedSearchField()
        {
            _Id = Guid.NewGuid();
        }
    }
}