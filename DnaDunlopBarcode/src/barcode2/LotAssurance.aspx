<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    Inherits="DunlopBarcode.Web.LotAssurance" Title="Dunlop Barcode | Curing Lot Assurance" Codebehind="LotAssurance.aspx.cs" %>
<%@ Register src="ExcelExport.ascx" tagname="ExcelExport" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#<%= DateTextBox.ClientID %>").datepicker();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2>
            Curing Lot Assurance</h2>
        <div>
            <label>
                Date</label>
            <asp:TextBox ID="DateTextBox" runat="server"></asp:TextBox>
            
            <label>Machine</label>
            <asp:DropDownList ID="MachineDropDownList" runat="server">
            </asp:DropDownList>
            <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
            <uc1:ExcelExport ID="ExcelButton" ListViewSource="ListView1" runat="server" />
        </div>
        
        <div class="searchResults clear">
        <asp:ListView runat="server" ID="ListView1">
        <EmptyDataTemplate>
                <div class="notice loud large">No data found.</div></EmptyDataTemplate>
               
            <LayoutTemplate>
                <table id="Table1" runat="server">
                    <thead>
                        <tr>
                            <th>Machine Name</th>
                            <th>Mold Number</th>
                            <th>Green Tire Number</th>
                            <th>Sku Number</th>
                            <th>Cure Number</th>
                            <th>Operator Name</th>
                            <th>Employee Number</th>
                            <%--<th>Operator Serial (Badge) Number </th>--%>
                            <th>Date</th>
                            <th>Tire Serial Number</th>
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
                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("machine_name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("mold_num") %>' />
                    </td>
                    
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("green_tire_number") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("sku_num") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("cure_num") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("operator_name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("employee_number") %>' />
                    </td>
                    <%--<td>
                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("operator_serial_number") %>' />
                    </td>--%>
                    <td style="white-space:nowrap">
                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("created", "{0:yyyy-MM-dd HH:mm:ss}") %>' />
                    </td>
                    <td>
                        <a href='EventSearch.aspx?t=p&l=<%#Eval("part_serial_number") %>'><asp:Label ID="Label5" runat="server" Text='<%#Eval("part_serial_number") %>' /></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
        
        
        <%--        <p>
        </p>
        <hr />
        <div class="column span-24 large last">
            <div class="column span-11 colborder">
                <label>
                    Left Cavity Number</label>
                
            </div>
            <div class="column span-11 last">
                <label>
                    Right Cavity Number</label>
                <asp:DropDownList ID="MachineRightDropDownList" runat="server">
                </asp:DropDownList>
            </div>
            <div class="column span-11 colborder">
                <label>
                    Mold Number</label></div>
            <div class="column span-11 last">
                <label>
                    Mold Number</label></div>
            <div class="column span-11 colborder">
                <label>
                    Green Tire Number</label></div>
            <div class="column span-11 last">
                <label>
                    Green Tire Number</label></div>
            <div class="column span-11 colborder">
                <label>
                    Cure ID Number</label></div>
            <div class="column span-11 last">
                <label>
                    Cure ID Number</label></div>
        </div>
        <hr class="space" />
        <hr />
--%>
    </div>
</asp:Content>
