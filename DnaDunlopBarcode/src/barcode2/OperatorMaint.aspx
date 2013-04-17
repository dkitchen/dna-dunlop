<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="OperatorMaint.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="DunlopBarcode.Web.OperatorMaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        table.dna-table td, 
        table.dna-table th
        {
            font-family: Sans-Serif;
            
        }
        table.dna-table th
        {
            background-color: #369;
            color: #eee;
            font-weight:bold;
            padding:1em;
        }
        table.dna-table tr.odd
        {
            background-color: #eee;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel EnableViewState="false" ID="MessagePanel" CssClass="" runat="server">
        <asp:Literal ID="MessageLiteral" runat="server" EnableViewState="false" ></asp:Literal>
    </asp:Panel>
    <asp:ListView ID="ListView1" DataKeyNames="operator_id" runat="server" 
        onitemediting="ListView1_ItemEditing" 
        onitemcanceling="ListView1_ItemCanceling" 
        onitemupdating="ListView1_ItemUpdating">
        <EmptyDataTemplate>
            No data found.</EmptyDataTemplate>
        <LayoutTemplate>
            <table class="dna-table">
                <thead>
                    <tr>
                        <th>
                            Name
                        </th>
                         <th>
                            Employee Number
                        </th>
                        <th>
                            Badge Serial Number
                        </th>
                    </tr>
                </thead>
                <tbody runat="server" id="itemPlaceholder">
                </tbody>
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <asp:LinkButton ID="LinkButton1" Text='<%# Eval("NAME") %>' CommandName="Edit" runat="server"></asp:LinkButton>
                    
                </td>
                <td>
                    <%# Eval("EMPLOYEE_NUMBER") %>
                </td>
                 <td>
                    <%# Eval("SERIAL_NUMBER") %>
                </td>
            </tr>
        </ItemTemplate>
        <%--<AlternatingItemTemplate>
            <tr class="odd">
                <td>
                    <%# Eval("NAME") %>
                </td>
            </tr>
        </AlternatingItemTemplate>--%>
        <EditItemTemplate>
        <tr class="edit">
            <tr>
                <td>
                    <%# Eval("NAME") %>
                </td>
                <td>
                    <%# Eval("EMPLOYEE_NUMBER") %>
                </td>
                 <td>
                     <asp:TextBox ID="SerialNumberTextBox" Text='<%# Eval("SERIAL_NUMBER") %>' runat="server"></asp:TextBox>
                     <asp:Button ID="Button1" CommandName="Update" runat="server" Text="Save" />
                     <asp:Button ID="Button2" CommandName="Cancel" runat="server" Text="Cancel" />
                </td>
            </tr>
        </tr>
        </EditItemTemplate>
    </asp:ListView>
</asp:Content>
