<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="internAttendance.aspx.cs"
    Inherits="kfmc.internAttendance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <asp:Label ID="lblSecurity" runat="server" Text="Only Authorized Users" Visible="false"></asp:Label>
    <div id="Filter" runat="server" visible="false">
        <asp:Label ID="lblTrainingTerm" runat="server" Text="Training Term" CssClass="myFormCaption"></asp:Label>
        <asp:DropDownList ID="ddlTrainingTermFilter" runat="server" AppendDataBoundItems="True"
            CssClass="myFormInputControls">
        </asp:DropDownList>
        <br />

        <asp:Label ID="Label1" runat="server" Text="Training Year " CssClass="myFormCaption"></asp:Label>
        <asp:DropDownList ID="ddlTrainingYearFilter" runat="server" AppendDataBoundItems="True"
            CssClass="myFormInputControls" AutoPostBack="True" >
        </asp:DropDownList>
        <br />
        <asp:Button ID="btnGetData" runat="server" Text="Get Data"  CssClass="myBtnSubmit" OnClick="btnGetData_Click" />
        <br />
    </div>
       <div class="myContent" id ="securityContent" runat="server">
        <fieldset class="InputForm" style="width: 1200px">
            <legend style="px"><strong>Intern Attendance</strong></legend>
            <table class="nav-justified myUserInput" style="width: 1200px">
                <tr>
                    <td >
                        <asp:Label ID="lblOutput" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="myUserInput" style="height: 21px" >Intern&nbsp; List</td>
                </tr>
                <tr style="vertical-align: top">
                    <td id="cblEmployeeCss"  vertical-align: top;">
                        <asp:CheckBoxList ID="cblIntern" runat="server" checked="checked" RepeatDirection="Horizontal" RepeatColumns="5"
                            OnSelectedIndexChanged="cblIntern_SelectedIndexChanged" >                           
                        </asp:CheckBoxList>
                        <asp:LinkButton ID="LBtn" runat="server" Visible="false"
                            OnCommand="Lbtn_Click"
                            CommandArgument="525"
                            CommandName="LinkButon"
                            Text="InternReport">
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
                        <asp:Button ID="btnShowInternAttendanceLog" runat="server"  Text="Show Intern Attendance Log"
                            ViewStateMode="Enabled" OnClick="btnShowInternAttendanceLog_Click" />
                        
                    </td>
                </tr>
            </table>
        </fieldset>
               <div class="myGv">
            <fieldset class="gvStyle">
                <legend><strong>Intern Attendance Log</strong></legend>
                <asp:Label ID="lblInternCount" runat="server" Text=""></asp:Label>
                <asp:GridView ID="gvInternAttendanceLog" runat="server" AutoGenerateColumns="False" DataKeyNames="internid"
                    ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    GridLines="both" CssClass="gv"
                    EmptyDataRowStyle-ForeColor="Red"
                    AllowSorting="true"
                    OnSorting="gvInternAttendanceLog_Sorting"
                    AllowPaging="true"
                    OnPageIndexChanging="gvInternAttendanceLog_PageIndexChanging"
                    PageSize="30">
                    <Columns>
                        <asp:BoundField DataField="internId" HeaderText="internId" InsertVisible="False" ReadOnly="True" SortExpression="internId" />
                        <asp:TemplateField HeaderText="Name 1" InsertVisible="false" SortExpression="internName" Visible="false">
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:HyperLink ID="InternLink"
                                    NavigateUrl='<%# "~/internship/internAttendance.aspx?internId=" + DataBinder.Eval(Container.DataItem,"internId") %>'
                                    Text='<%# DataBinder.Eval(Container.DataItem,"internName") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" InsertVisible="false" SortExpression="internName">
                            <ItemStyle />
                            <ItemTemplate>
                      <%--   <asp:LinkButton ID="lblViewInternProfile" 
                                    OnClick="callMethodFromGUI"
                                    Text='<%# DataBinder.Eval(Container.DataItem,"internId") %>'
                                    CommandArgument='<%# Eval("internId") %>' 
                                    CommandName="select"
                                    ForeColor="#8C4510" runat="server"></asp:LinkButton>--%>
                                <asp:LinkButton ID="LbnInternDetail"
                                    OnClick="InternDetail"
                                    Text='<%# DataBinder.Eval(Container.DataItem,"internName") %>'
                                    CommandArgument='<%# Eval("internId") %>'
                                    CommandName="select" 
                                    ForeColor="#8C4510" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="workDate" HeaderText="workDate" SortExpression="workDate" />
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
    </div>

        <div>
        <asp:Button ID="Button1" runat="server" Text="fill form"  Visible="false"/>
        <%--    code from 89 til 100 is modal popup--%>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" 
            CancelControlID="btnCancel"
            TargetControlID="Button1" 
            PopupControlID="Panel1">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" style = "display:none">
        <iframe style=" width: 350px; height: 200px;" id="irm1" src="changePwd.aspx" runat="server"></iframe>
        <br/>
        <asp:Button ID="Button2" runat="server" Text="Close" />
        </asp:Panel>
    </div>
</asp:Content>
