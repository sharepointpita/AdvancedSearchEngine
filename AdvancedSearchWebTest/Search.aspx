<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="AdvancedSearchWebTest.Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>Advanced Search Control Example</title>

    <style type="text/css">
    
    .ContainerSearchCriteria
    {
        width:800px;
    }
    
    </style>

</head>

<body>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>

    <div class="ContainerSearchCriteria">
        <asp:UpdatePanel ID="updPnl" runat="server" style="width:100%" >
            <ContentTemplate>

                <asp:DataList runat="server" ID="dlSearchCriteria"  style="width:100%">
                    <HeaderTemplate>
                        <table width="100%" cellpadding="0" cellspacing="0" border="1px">
                        <tr style="background-color:Maroon; color:White;">
                            <th align="left" style="width:30%">
                                Field:
                            </th>
                            <th align="left" style="width:30%">
                                Operator:
                            </th>
                            <th align="left" style="width:30%">
                                Value:
                            </th>
                            <th align="center" style="width:10%">
                               
                            </th>
                        </tr>
                    </HeaderTemplate>

                    <ItemTemplate>
                            <tr runat="server" id='trItemTemplate' >
                                <td>
                                   <asp:Label id="lblId" Visible="false" runat="server" Text='<%# Eval("Id") %>' />
                                   <asp:Label id="lblFieldDisplayName" runat="server" Text='<%# Eval("fieldDisplayName") %>' />
                                </td>    
                                   
                                <td>
                                      <asp:Label id="lblOperatorDisplayName" runat="server" Text='<%# Eval("operatorDisplayName") %>' />
                                </td>    

                                <td>
                                      <asp:Label id="lblUserFriendlyValue" runat="server" Text='<%# Eval("UserFriendlyValue") %>' />
                                </td> 
                                <td align="center">
                                <asp:ImageButton runat="server" ID="btnDelete" Text="Delete search criteria" CommandName="DeleteSearchCriteria" ImageUrl="~/Images/delete2.ico" Height="24" Width="24" />
                                </td>   
                            </tr>
                    </ItemTemplate>

                   <%-- <EditItemTemplate>
                            <tr style="background-color:Gray;">
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlFields" />
                                </td>    
                                   
                                <td>
                                     
                                    <asp:DropDownList runat="server" ID="ddlOperator" />
                                </td>    

                                <td>
                                    
                                    <asP:literal runat="server" ID="litValueContainer" />
                                </td>    
                            </tr>
                    </EditItemTemplate>--%>

                    <FooterTemplate>
                        <tr style="background-color:Gray;">
                                <td>
                                   
                                    <asp:DropDownList runat="server" ID="ddlFields" AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged"  />
                                </td>    
                                   
                                <td>
                                     
                                    <asp:DropDownList runat="server" ID="ddlOperators" />
                                </td>    

                                <td>
                                    <asp:Panel runat="server" ID="pnlvalueContainer" />
                                </td> 
                                <td align="center">
                                <%--<asp:Button runat="server" ID="btnAdd" Text="Add search criteria" CommandName="addNewCriteria" />--%>
                                <asp:ImageButton runat="server" ID="btnAdd" Text="Add search criteria" CommandName="addNewCriteria" ImageUrl="~/Images/add.ico" Height="24" Width="24" />
                                </td>      
                            </tr>
                        </table>
                    </FooterTemplate>
                
                </asp:DataList>
               
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <br />
    <br />
    
    <asp:RadioButtonList runat="server" ID="rblAndOr" RepeatLayout="Flow" RepeatDirection="Horizontal">
        <asp:ListItem Selected="True" Text="OR" Value="OR" />
        <asp:ListItem  Text="AND" Value="AND" />
    </asp:RadioButtonList>

    <br />
    <br />

    <asp:Button runat="server" ID="btnDoSearch" Text="Search" OnClick="btnDoSearch_OnClick" />

    <br />
    <br />

    <asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Rows="20" ReadOnly="true" Width="800" Text="Press the search button to generate SQL"></asp:TextBox>

    <br />
    <br />

    <asp:GridView runat="server" ID="grdDynamic" AutoGenerateColumns="true">
    </asp:GridView>

    <asp:Label runat="server" ID="lblNoDataSourceResult" visible="false" Text="Query doesn't result any data" />

    </form>
</body>
</html>
