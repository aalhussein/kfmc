<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="internTrainingSchedule.aspx.cs"
    Inherits="kfmc.internship.internTrainingSchedule" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblOutput" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblCaption" runat="server" Text="Please  make your selection  and enter your email address to get your training schedule"></asp:Label>
    <br />
    <br />
      <div id="Filter" runat="server" visible="true">
        <asp:Label ID="lblTrainingTerm" runat="server" Text="Training Term" CssClass="myFormCaption"></asp:Label>
        <asp:DropDownList ID="ddlTrainingTermFilter" runat="server" AppendDataBoundItems="True"
            CssClass="myFormInputControls" OnSelectedIndexChanged="ddlTrainingTermFilter_SelectedIndexChanged">
        </asp:DropDownList>
        <br />

        <asp:Label ID="Label1" runat="server" Text="Training Year " CssClass="myFormCaption"></asp:Label>
        <asp:DropDownList ID="ddlTrainingYearFilter" runat="server" AppendDataBoundItems="True"
            CssClass="myFormInputControls" AutoPostBack="True" OnSelectedIndexChanged="ddlTrainingYearFilter_SelectedIndexChanged" >
        </asp:DropDownList>
        <br />
    <asp:TextBox ID="txtEmail" runat="server" Width="289px"  CssClass="myFormInputControls"></asp:TextBox>
    <asp:CheckBox ID="cbxGetSingleSchedule" runat="server" Visible="false"  CssClass="myFormInputControls" />
    <asp:Button ID="btnGetTrainingSchedule" runat="server" Text="Get Training Schedule "  CssClass="myFormInputControls"
        OnClick="btnGetTrainingSchedule_Click" /><br />
    </div>
           
    <fieldset class="InputForm">
        <legend><strong>Intern Training Weekly Schedule</strong></legend>
        <asp:GridView ID="gvInternTrainingSchedule" runat="server" AutoGenerateColumns="False" DataKeyNames="InternId"
            OnRowDataBound="gvInternTrainingSchedule_RowDataBound"
            OnRowCommand="gvInternTrainingSchedule_RowCommand"
            OnSorting="gvInternTrainingSchedule_Sorting"
            AllowPaging="false" AllowSorting="true"
            OnPageIndexChanging="gvInternTrainingSchedule_PageIndexChanging"
            PageSize="50"
            OnSelectedIndexChanged="gvInternTrainingSchedule_SelectedIndexChanged"
            CssClass="gvStyle "
            AlternatingRowStyle-CssClass="alt"
            PagerStyle-CssClass="pgr">
            <Columns >
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbEdit" CommandArgument='<%# Eval("InternId") %>' CommandName="EditRow"
                            ForeColor="#8C4510" runat="server" CssClass="myEditBtnGv">Edit</asp:LinkButton>
                        <asp:LinkButton ID="lbDelete" CommandArgument='<%# Eval("InternId") %>' CommandName="DeleteRow"
                            OnClientClick="return confirm('Are you sure to delete');" ForeColor="#8C4510"
                            runat="server" CssClass="myDeleteBtnGv" CausesValidation="false" Visible="false">Delete</asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="lbUpdate" CommandArgument='<%# Eval("InternId") %>' CommandName="UpdateRow"
                            ForeColor="#8C4510" runat="server" CssClass="myUpdateBtnGv">Update</asp:LinkButton>
                        <asp:LinkButton ID="lbCancel" CommandArgument='<%# Eval("InternId") %>' CommandName="CancelUpdate"
                            ForeColor="#8C4510" runat="server" CssClass="myCancelBtnGv" CausesValidation="false">Cancel</asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="refNo" HeaderText="Ref No" SortExpression="RefNo" ReadOnly="true" />
                <%--                <asp:TemplateField HeaderText="Name" InsertVisible="False" SortExpression="name"
                    Visible="true">
                    <EditItemTemplate>
                        <asp:Label ID="lblEditName" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
<asp:TemplateField HeaderText="Name" InsertVisible="false" SortExpression="name">
    <ItemStyle />
    <ItemTemplate>
<%--        <asp:ImageButton ID="ImageButton1" runat="server" PostBackUrl='<%# "~/internship/internProfile.aspx?internId=" + DataBinder.Eval(Container.DataItem,"internId")  %>'
            ImageUrl="~/images/clickMe.PNG" Width="20px" Height="20px" />--%>
     
        <asp:LinkButton ID="LbViewProfile" runat="server" CommandArgument='<%# Eval("InternId") %>' CommandName="viewProfile"
             OnClientClick="NewWindow();" 
              Text='<%# DataBinder.Eval(Container.DataItem,"name") %>' ToolTip="Click to view Intern Profile">ViewProfile</asp:LinkButton>
    
<%--        <asp:HyperLink ID="InternLink"  
            NavigateUrl='<%# "~/internship/internProfile.aspx?internId=" + DataBinder.Eval(Container.DataItem,"internId")  %>'
            Text='<%# DataBinder.Eval(Container.DataItem,"name") %>' runat="server" />--%>
    </ItemTemplate>
</asp:TemplateField>

                <asp:TemplateField HeaderText="Sunday" SortExpression="sunday">
                    <EditItemTemplate>
                        <asp:CheckBox ID="cbSunEdit" Checked='<%# Bind("sunday") %>' runat="server" Enabled="true" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbSun" Checked='<%# Bind("sunday") %>' runat="server" Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Monday" SortExpression="monday">
                    <EditItemTemplate>
                        <asp:CheckBox ID="cbMonEdit" Checked='<%# Bind("monday") %>' runat="server" Enabled="true" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbMon" Checked='<%# Bind("monday") %>' runat="server" Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Tuesday" SortExpression="tuesday">
                    <EditItemTemplate>
                        <asp:CheckBox ID="cbTueEdit" Checked='<%# Bind("tuesday") %>' runat="server" Enabled="true" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbTue" Checked='<%# Bind("tuesday") %>' runat="server" Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Wednesday" SortExpression="wednesday">
                    <EditItemTemplate>
                        <asp:CheckBox ID="cbWedEdit" Checked='<%# Bind("wednesday") %>' runat="server" Enabled="true" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbWed" Checked='<%# Bind("wednesday") %>' runat="server" Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Thursday" SortExpression="thursday">
                    <EditItemTemplate>
                        <asp:CheckBox ID="cbThuEdit" Checked='<%# Bind("thursday") %>' runat="server" Enabled="true" />
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbThu" Checked='<%# Bind("thursday") %>' runat="server" Enabled="False" />
                    </ItemTemplate>
                </asp:TemplateField>



            </Columns>
        </asp:GridView>
    </fieldset>

    <script type="text/javascript">
         function NewWindow() {
             document.forms[0].target = "_blank";
         }
 </script>
</asp:Content>
