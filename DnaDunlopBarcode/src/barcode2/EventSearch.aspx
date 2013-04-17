<%@ Page Title="Dunlop Barcode | Event Search" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="True" CodeBehind="EventSearch.aspx.cs" Inherits="DunlopBarcode.Web.EventSearch"
    EnableEventValidation="false" %>

<%@ Register Src="ExcelExport.ascx" TagName="ExcelExport" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(function() {
           $("#<%= StartDateTextBox.ClientID %>").datepicker();
           $("#<%= EndDateTextBox.ClientID %>").datepicker();
           $("#<%= NoCuringStartTextBox.ClientID %>").datepicker();
           $("#<%= PartSerialNumberTextBox.ClientID %>").focus();
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container" id="searchToolbar">
        <h2>
            <asp:Label ID="HeadLabel" runat="server" Text="Label"></asp:Label></h2>
    
        <div id="NoCuringDataDate" runat="server">
            <div class="column span-3 colborder">
                <label for='<%= NoCuringStartTextBox.ClientID %>'>
                    End Date</label>
            </div>
            <div class="column span-17 last">
                <asp:TextBox ID="NoCuringStartTextBox" runat="server"></asp:TextBox>
                
            </div>
        </div>
        <div id="PartSerialSearch" runat="server" >
            <div class="column span-3 colborder">
            <label for='<%= PartSerialNumberTextBox.ClientID %>'>
                Tire Label</label>
        
            </div>
            <div class="column span-17 last">
                <asp:TextBox ID="PartSerialNumberTextBox" runat="server"></asp:TextBox>
            </div>
        </div>
        <div id="FullEventSearch" runat="server">
            <div class="column span-3 colborder">
                <label for='<%= MachineDropDownList.ClientID %>'>
                    Machine</label>
            </div>
            <div class="column span-17 last">
                <asp:DropDownList ID="MachineDropDownList" runat="server">
                </asp:DropDownList>
            </div>
            <div class="column span-3 colborder">
                <label for='<%= OperaotrDropDownList.ClientID %>'>
                    Operator</label>
            </div>
            <div class="column span-17 last">
                <asp:DropDownList ID="OperaotrDropDownList" runat="server">
                </asp:DropDownList>
            </div>
            <div class="column span-3 colborder">
                <label for='<%= EventDropDownList.ClientID %>'>
                    Event</label>
            </div>
            <div class="column span-17 last">
                <asp:DropDownList ID="EventDropDownList" runat="server">
                </asp:DropDownList>
            </div>
            <div class="column span-3 colborder">
                <label for='<%= EventDataDropDownList.ClientID %>'>
                    Event Data</label>
            </div>
            <div class="column span-17 last">
                <asp:DropDownList ID="EventDataDropDownList" runat="server">
                </asp:DropDownList>
            </div>
            <div class="column span-3 colborder">
                <label for='<%= GreenTireNumberDropDownList.ClientID %>'>
                    Green Tire</label>
            </div>
            <div class="column span-17 last">
                <asp:DropDownList ID="GreenTireNumberDropDownList" runat="server">
                </asp:DropDownList>
            </div>
            <div class="column span-3 colborder">
                <label>
                    Date Time Range</label>
            </div>
            <div class="column span-17 last">
                <label for='<%= StartDateTextBox.ClientID %>'>
                    Start</label>
                <asp:TextBox ID="StartDateTextBox" runat="server"></asp:TextBox>
                <asp:DropDownList ID="StartHourDropDownList" ToolTip="Hour" runat="server">
                </asp:DropDownList>
                :
                <asp:DropDownList ID="StartMinuteDropDownList" ToolTip="Minute" runat="server">
                </asp:DropDownList>
                </div>
            <div class="column prepend-4 span-17 last">
                <label for='<%= EndDateTextBox.ClientID %>'>
                    End&nbsp;&nbsp;</label>
                <asp:TextBox ID="EndDateTextBox" runat="server"></asp:TextBox>
                <asp:DropDownList ID="EndHourDropDownList" ToolTip="Hour" runat="server">
                </asp:DropDownList>
                :
                <asp:DropDownList ID="EndMinuteDropDownList" ToolTip="Minute" runat="server">
                </asp:DropDownList>
            </div>
            <div class="column span-3 colborder">
                <label for='<%= ShiftDropDownList.ClientID %>'>
                    Shift</label>
            </div>
            <div class="column span-17 last">
                <asp:DropDownList ID="ShiftDropDownList" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <div class="column span-3 colborder">
            <label for='<%= RowLmitDropDownList.ClientID %>'>
                Results Per Page</label>
        </div>
        <div class="column span-17 last">
            <asp:DropDownList ID="RowLmitDropDownList" runat="server">
                <asp:ListItem>100</asp:ListItem>
                <asp:ListItem>500</asp:ListItem>
                <asp:ListItem>1000</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="SearchButton_Click" />
            &nbsp;&nbsp;
            <uc1:ExcelExport ID="ExcelButton" ListViewSource="ListView1" runat="server" />
            &nbsp;&nbsp; <span id="ClearButton" runat="server"><a href="EventSearch.aspx">Clear</a></span>
        </div>
    </div>
    <div class="searchResults clear">
        <asp:HiddenField ID="SortExpressionHiddenField" runat="server" />
        <asp:ListView runat="server" ID="ListView1" OnSorting="ListView1_Sorting">
            <EmptyDataTemplate>
                <div class="notice loud large">
                    No data found.</div>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <thead>
                        <tr>
                            <th>
                                <asp:LinkButton ID="IDLinkButton" Text="ID &#9660;" CommandName="Sort" CommandArgument="event_log_id ASC"
                                    runat="server"></asp:LinkButton>
                            </th>
                            <th>
                                Event Date
                            </th>
                            <%--<th>Shift Date</th>--%>
                            <th>
                                Shift
                            </th>
                            <th>
                                Event
                            </th>
                            <th>
                                Machine
                            </th>
                            <th>
                                Tire Create Date
                            </th>
                            <th>
                                Tire Serial Number
                            </th>
                            <th>
                                Green Tire Number
                            </th>
                            <th>
                                Goodyear Serial Number
                            </th>
                            <th>
                                Operator
                            </th>
                            <th>
                                Data Name
                            </th>
                            <th>
                                Data Value
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
                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("Event_Log_Id") %>' />
                    </td>
                    <td style="white-space: nowrap">
                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Event_Created", "{0:yyyy-MM-dd HH:mm:ss}") %>' />
                    </td>
                    <%--<td>
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Work_Date") %>' />
                    </td>--%>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Work_Shift") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Event_Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Machine_Name") %>' />
                    </td>
                    <td style="white-space: nowrap">
                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Part_Created", "{0:yyyy-MM-dd HH:mm:ss}") %>' />
                    </td>
                    <td>
                        <a href='EventSearch.aspx?t=p&l=<%#Eval("Part_Label") %>'><asp:Label ID="Label7" runat="server" Text='<%#Eval("Part_Label") %>' /></a>
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("Green_Tire_Number") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text='<%#Eval("Goodyear_Serial_Number") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("Operator") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("Data_Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text='<%#Eval("Data_Value") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </div>
    
    <div id="TracingCheckbox" runat="server" visible="false" class="container quiet">
        <label for='<%= TracingEnabledCheckBox.ClientID %>'>
            Tracing Enabled</label>
        <asp:CheckBox ID="TracingEnabledCheckBox" runat="server" />
    </div>
</asp:Content>
