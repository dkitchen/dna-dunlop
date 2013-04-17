<%@ Page Title="Dunlop Barcode | Machine Label Score" Language="C#" MasterPageFile="~/Site.Master" 
AutoEventWireup="true" Inherits="DunlopBarcode.Web.MachineLabelScore" Codebehind="MachineLabelScore.aspx.cs" %>
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
        <h2>Machine Label Score</h2>
        <p>Indicates total number of tires scanned at Finishing that were successfully read vs. mis-read. Low percentage may indicate improper tire, label, or scanner placement.</p>
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
    
    <div class="searchResults clear container">
        <asp:ListView runat="server" ID="ListView1">
        <EmptyDataTemplate>
                <div class="notice loud large">No data found.</div></EmptyDataTemplate>
            
            <LayoutTemplate>
                <table id="Table1" runat="server">
                    <thead>
                        <tr>
                            <th>Machine</th>
                            <th>Reads</th>
                            <th>Good Reads</th>
                            <th>Bad Reads</th>
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
                    
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("machine_name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("reads") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("good_reads") %>' />
                    </td>
                    
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("bad_reads") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("pct") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
</asp:Content>
