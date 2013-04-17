<%@ Page Title="Dunlop Barcode | Machine Totals" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" CodeBehind="MachineTotals.aspx.cs" Inherits="DunlopBarcode.Web.MachineTotals" %>

<%@ Register Src="ExcelExport.ascx" TagName="ExcelExport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#<%= StartDateTextBox.ClientID %>").datepicker();
            $("#<%= EndDateTextBox.ClientID %>").datepicker();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div id="searchToolbar clear span-15 last">
        <h2>
            Machine Totals</h2>
        Department:
        <asp:DropDownList ID="DepartmentDropDownList" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="DepartmentDropDownList_SelectedIndexChanged">
        </asp:DropDownList>
        Machine:
        <asp:DropDownList ID="MachineDropDownList" runat="server">
        </asp:DropDownList>
        Start:
        <asp:TextBox ID="StartDateTextBox" runat="server"></asp:TextBox>
        End:
        <asp:TextBox ID="EndDateTextBox" runat="server"></asp:TextBox>
        <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
        <uc1:ExcelExport ID="ExcelButton" ListViewSource="ListView1" runat="server" />
        
    </div> 
    
    
        
    <div class="searchResults span-10 prepend-5" style="margin-top:10px;">
        <p>
        <asp:ListView runat="server" ID="ListView1">
            <EmptyDataTemplate>
                <div class="notice loud large">
                    No data found.</div>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table id="Table1" runat="server">
                    <thead>
                        <tr>
                            <th>
                                Machine
                            </th>
                            <th>
                                Date
                            </th>
                            <th>
                                Shift
                            </th>
                            <th>
                                Total
                            </th>
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
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("machine_name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("work_date", "{0:yyyy-MM-dd}") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("shift") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("Total") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        </p>
    </div>
    
    <p>
    <asp:Panel ID="TotalPanel" runat="server">
    
        <div class="span-5 notice">
        <div class="span-5 last"><h4><strong>TOTALS</strong></h4></div>
        <div class="span-2">
                <label>A Shift</label></div>
            <div class="span-3 last">
                <asp:Label ID="AShiftTotalLabel" runat="server" Text=""></asp:Label></div>
            <div class="span-2">
                <label>B Shift</label></div>
            <div class="span-3 last">
                <asp:Label ID="BShiftLabel" runat="server" Text=""></asp:Label></div>
            <div class="span-2">
                <label>C Shift</label></div>
            <div class="span-3 last">
                <asp:Label ID="CShiftLabel" runat="server" Text=""></asp:Label></div>
            <div class="span-2" style="border-top:solid 1px #999;">
                <label>All Shifts</label></div>
            <div class="span-3 last" style="border-top:solid 1px #999;">
                <asp:Label ID="DailyTotalLabel" runat="server" Text=""></asp:Label></div>
        </div>
    </asp:Panel>
    </p>
        
    </div>
</asp:Content>
