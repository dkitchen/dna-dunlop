<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    Inherits="DunlopBarcode.Web.FinishingPivot" Title="Dunlop Barcode | Finishing Matrix" Codebehind="FinishingPivot.aspx.cs" %>
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
            Finishing Report</h2>
        <div>
            <label>
                Date</label>
            <asp:TextBox ID="DateTextBox" runat="server"></asp:TextBox>
            
            <label>Machine</label>
            <asp:DropDownList ID="MachineDropDownList" runat="server">
            </asp:DropDownList>
            
            <label>Green Tire Number</label>
            <asp:DropDownList ID="GreenTireNumberDropDownList" runat="server">
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
                            <th>Green Tire Number</th>
                            <th>Date</th>
                            <th>Tire Serial Number</th>
                            <th>Tire Grade</th>
                            <th>Bottom Peak to Peak (in)</th>
                            <th>Top Peak to Peak (in)</th>
                            <th>Radial Peak to Peak (in)</th>
                            <th>Radial 1st Harmonic (in)</th>
                            
                            <th>Upper Magnitude (oz)</th>
                            <th>Lower Magnitude (oz)</th>
                            <th>Couple Magnitude (oz in2)</th>
                            <th>Static Magnitude (oz in)</th>
                            <th>Static Angle (deg)</th>
                            <th>Weight (lbs)</th>
                            
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
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("green_tire_number") %>' />
                    </td>
                    
                    <td style="white-space:nowrap">
                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("created", "{0:yyyy-MM-dd HH:mm:ss}") %>' />
                    </td>
                    <td>
                        <a href='EventSearch.aspx?t=p&l=<%#Eval("part_serial_number") %>'><asp:Label ID="Label5" runat="server" Text='<%#Eval("part_serial_number") %>' /></a>
                    </td>
                    <td><asp:Label ID="Label1" runat="server" Text='<%#Eval("TIRE_GRADE") %>' /></td>
                    
                    <td><asp:Label ID="Label6" runat="server" Text='<%#Eval("BOTTOM_PEAK") %>' /></td>
                    <td><asp:Label ID="Label8" runat="server" Text='<%#Eval("TOP_PEAK") %>' /></td>
                    <td><asp:Label ID="Label10" runat="server" Text='<%#Eval("RADIAL_PEAK") %>' /></td>
                    <td><asp:Label ID="Label9" runat="server" Text='<%#Eval("RADIAL_1ST_HARM") %>' /></td>
                    
                    
                    <td><asp:Label ID="Label2" runat="server" Text='<%#Eval("UPPER_MAG") %>' /></td>
                    <td><asp:Label ID="Label12" runat="server" Text='<%#Eval("LOWER_MAG") %>' /></td>
                    <td><asp:Label ID="Label13" runat="server" Text='<%#Eval("COUPLE_MAG") %>' /></td>
                    
                    <td><asp:Label ID="Label11" runat="server" Text='<%#Eval("STATIC_MAG") %>' /></td>
                    <td><asp:Label ID="Label4" runat="server" Text='<%#Eval("STATIC_ANGLE") %>' /></td>
                    <td><asp:Label ID="Label14" runat="server" Text='<%#Eval("WEIGHT") %>' /></td>
                    
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
