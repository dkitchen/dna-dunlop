<%@ Page Title="Dunlop Barcode | Operator Label Score" Language="C#" MasterPageFile="~/Site.Master" 
AutoEventWireup="true" Inherits="DunlopBarcode.Web.OperatorFinishScanTotals" Codebehind="OperatorFinishScanTotals.aspx.cs" %>
<%@ Register src="ExcelExport.ascx" tagname="ExcelExport" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#<%= StartDateTextBox.ClientID %>").datepicker();
            $("#<%= EndDateTextBox.ClientID %>").datepicker();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container" id="searchToolbar">
        <h2>Operator Label Score</h2>
        <p>Indicates the percentage of parts built by an operator that were successfully scanned in finishing. Low score may indicate improper label placement.</p>
        Operator:
        <asp:DropDownList ID="OperatorDropDownList" runat="server">
        </asp:DropDownList>
        Start:
        <asp:TextBox ID="StartDateTextBox" runat="server"></asp:TextBox>
        End:
        <asp:TextBox ID="EndDateTextBox" runat="server"></asp:TextBox>
        
        <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
        
        <uc1:ExcelExport ID="ExcelButton" ListViewSource="ListView1" runat="server" />
    </div>
    
    <div class="searchResults clear container">
        <asp:ListView runat="server" ID="ListView1">
        <EmptyDataTemplate>
                <div class="notice loud large">No data found.</div></EmptyDataTemplate>
            
            <LayoutTemplate>
                <table id="Table1" runat="server">
                    <thead>
                        <tr>
                            <%--<th>Serial (Badge) Number</th>--%>
                            <th>Employee Number</th>
                            <th>Operator Name</th>
                            <th>Build Total</th>
                            <th>Finish Total</th>
                            <th>%</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                    </tbody>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="<%# Container.DisplayIndex % 2 == 0 ? "even" : "odd" %>">
                    <%--<td>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("operator_serial_number") %>' />
                    </td>--%>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("employee_number") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("operator_name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="BuildTotalLabel" runat="server" Text='<%#Eval("build_total") %>' />
                    </td>
                    <td>
                        <asp:Label ID="FinishTotalLabel" runat="server" Text='<%#Eval("finish_total") %>' />
                    </td>
                    <td>
                        <asp:Label ID="PercentLabel" runat="server" Text='<%#Eval("pct") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
