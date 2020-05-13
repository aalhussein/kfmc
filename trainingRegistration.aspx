<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="trainingRegistration.aspx.cs"
    Inherits="kfmc.employee.trainingRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
            CancelControlID="btnCancel"
            TargetControlID="btnAddCourse"
            PopupControlID="Panel1">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" Style="display: none">
            <iframe style="width: 380px; height:260px;" id="irm1" 
              scrolling="no"   src="~/office/popUpCourse.aspx" runat="server"></iframe>
            <br />
            <asp:Button ID="btnCancel" runat="server" Text="Close" />
        </asp:Panel>
    </div>

    <%--    <link href="../style/gvStyle.css" rel="stylesheet" />--%>
    <div class="myContent">
        <fieldset class="InputForm">
            <legend><strong>Training Registration </strong></legend>
            <asp:Label ID="lblOutput" runat="server"></asp:Label><br />

            <asp:Label ID="lblName" runat="server" Text="First Name:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtFname" runat="server" TabIndex="100" CssClass="myFormInputControls"></asp:TextBox>
            <br />
            <asp:Label ID="lblMi" runat="server" Text="Middle Name:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtMi" runat="server" TabIndex="102" CssClass="myFormInputControls"></asp:TextBox>
            <br />
            <asp:Label ID="lblLName" runat="server" Text="Last Name:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtLname" runat="server"  TabIndex="103" CssClass="myFormInputControls"></asp:TextBox>
            <br />

            <asp:Label ID="lblContactType" runat="server" Text="Contact Type:" CssClass="myFormCaption"></asp:Label>
            <asp:DropDownList ID="ddlContactType" runat="server" CssClass="myFormInputControls" TabIndex="104"></asp:DropDownList>
            <br />
            <asp:Label ID="lblCell" runat="server" Text="Cell :" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtCell" runat="server" TabIndex="105" CssClass="myFormInputControls"></asp:TextBox>
            <br />

            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" TabIndex="106" CssClass="myFormInputControls"></asp:TextBox>
            <br />

            <asp:Label ID="lblactive" runat="server" Text="Active:" CssClass="myFormCaption"></asp:Label>
            <asp:CheckBox ID="cbActive" runat="server" TabIndex="107" CssClass="myFormInputControls"></asp:CheckBox>
            <br />

            <asp:Label ID="lblNote" runat="server" Text="Note:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtNote" runat="server" Height="71px" TextMode="MultiLine" Width="251px" CssClass="myFormInputControls" TabIndex="108"></asp:TextBox>
            <br />

            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="7" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="false" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
            <asp:Button ID="btnAddCourse" runat="server" Text="New Course" />
        </fieldset>
<div class="myGv">
<fieldset class="gvStyle">
    <legend><strong>Contact Information </strong></legend>

    <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="gv"
        EmptyDataText="No Records Found" GridLines="both" EmptyDataRowStyle-ForeColor="Red">
        <Columns>
            <asp:BoundField DataField="trainingRegistrationId" HeaderText="trainingRegistrationId" SortExpression=""
                    DataFormatString="" Visible="false" />

            <asp:TemplateField HeaderText="contact">
                <ItemTemplate>
                    <asp:Label ID="lblTrainingRegistrationId" runat="server" Text='<%#Eval("trainingRegistrationId") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lblFname" runat="server" Text='<%#Eval("fName") %>'></asp:Label>
                    <asp:Label ID="lblMi" runat="server" Text='<%#Eval("mI") %>'></asp:Label>
                    <asp:Label ID="lblLname" runat="server" Text='<%#Eval("lName") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="contact Type ID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblcontactTypeId" runat="server" Text='<%#Eval("contactTypeId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="contact Type">
                <ItemTemplate>
                    <asp:Label ID="lblcontactType" runat="server" Text='<%#Eval("contactType") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cell">
                <ItemTemplate>
                    <asp:Label ID="lblCell" runat="server" Text='<%#Eval("cell") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Active">
                <ItemTemplate>
                    <asp:CheckBox ID="cbActive" runat="server" Checked='<%#Eval("IsActive") %>' Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Note ">
                <ItemTemplate>
                    <asp:Label ID="lblNote" runat="server" Text='<%#Eval("note") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete"
                        OnClientClick="return confirm('Are you sure? want to delete !');"
                        OnClick="btnDelete_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</fieldset>
</div>
        <input type="hidden" runat="server" id="HdntrainingRegistrationId" />
    </div>
</asp:Content>
