<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Select.aspx.cs" Inherits="AdvancedSearchWebTest.Select" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Advanced Search Control Example</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>

    <div class="filterContainer">
        <span>Select a table to query:</span>
        <asp:DropDownList runat="server" ID="ddlTables" AutoPostBack="true" OnSelectedIndexChanged="ddlTables_SelectedIndexChanged"></asp:DropDownList>
    </div>

    <div class="columnsContainer">
        <div>Choose the columns to query:</div>
        <asp:CheckBoxList runat="server" ID="cblColumns">
        </asp:CheckBoxList>
    </div>

    <br />

    <asp:Button runat="server" ID="btnGoToFilter" Text="Filter these columns" OnClick="btnGoToFilter_Click"  />

    </form>
</body>
</html>
