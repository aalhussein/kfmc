<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="activityRegistration.aspx.cs"
    Inherits="kfmc.activityRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="myContent">
        <fieldset class="InputForm">
            <legend><strong>Employee Course Registration</strong></legend>
            <table class="nav-justified myUserInput myTable">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblOutput" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 19px; width: 351px;">&nbsp;</td>
                    <td style="height: 19px"></td>
                </tr>
                <tr>
                    <td style="width: 351px">Administration&nbsp;&nbsp;
    <asp:DropDownList ID="ddlAdmin" runat="server"
        OnSelectedIndexChanged="ddlAdmin_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>
                        &nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="myUserInput" style="width: 351px">Department &nbsp; &nbsp;<asp:DropDownList ID="ddlDep" runat="server"
                        OnSelectedIndexChanged="ddlDep_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    </td>
                    <td class="myUserInput">Activity: &nbsp;&nbsp;<asp:DropDownList ID="ddlActivityType" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlActivity_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="myUserInput" style="width: 351px">Employee List</td>
                    <td class="myUserInput">
                        <asp:Label ID="lblActivityName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="vertical-align: top">
                    <td id="cblEmployeeCss" style="width: 351px; vertical-align: top;">
                        <asp:CheckBoxList ID="cblEmployee" runat="server" checked="checked">
                            
                        </asp:CheckBoxList>
                    </td>
                    <td id="cblCourseCss" style="width: 351px">
                        <asp:CheckBoxList ID="cblActivity" runat="server" checked="checked">
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
                        <asp:Button ID="btnShowActivityLog" runat="server" OnClick="btnShowActivityLog_Click" Text="Show Activity Log"
                            ViewStateMode="Enabled" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <div class="myGv">
                        <fieldset class="gvStyle">
                <legend><strong>Employee Activity Log</strong></legend>
            <asp:GridView ID="gvActivityRegistration" runat="server">
            </asp:GridView>
                            </fieldset>
        </div>
    </div>
</asp:Content>
