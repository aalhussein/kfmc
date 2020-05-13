<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="employeeDirectory.aspx.cs"
    Inherits="kfmc.employee.employeeDirectory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="myContent">
        <fieldset class="InputForm">
            <legend><strong>Employee Directory</strong></legend>
            <table class="myTable">
                <tr>
                    <td style="font-family: Arial" colspan="2">
                        <asp:Label ID="lblOutput" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-family: Arial"><b>Administration</b></td>
                    <td>
                        <asp:DropDownList ID="ddlAdmin" runat="server"
                            OnSelectedIndexChanged="ddlAdmin_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="font-family: Arial"><b>Department</b></td>
                    <td>

                        <asp:DropDownList ID="ddlDep" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <asp:Button ID="btn" runat="server" Text="Get Employee" OnClick="btn_Click" />
        </fieldset>

        <fieldset class="gvStyle">
            <legend ><strong>Employee Information </strong></legend>
            <asp:GridView ID="gvEmployee" runat="server"></asp:GridView>
        </fieldset>
        </div>
</asp:Content>
