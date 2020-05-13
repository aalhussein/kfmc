<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InstitutionAdvisor.aspx.cs" 
    Inherits="kfmc.internship.InstitutionAdvisor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <fieldset class="InputForm">
            <legend>I<strong>nstitution Advisor</strong></legend>
            <asp:Label ID="lblOutput" runat="server" Text="" CssClass="myFormOutput" ></asp:Label>
                     <br />
            <br />
            <asp:HiddenField ID="txtInstitutionAdvisor" Value="employeeNewAcct" runat="server" />
            <asp:Label ID="lblinstitutionId" runat="server" Text="Institution Id" CssClass="myFormCaption"></asp:Label>
<%--            <asp:HyperLink ID="hyperLink" runat="server" NavigateUrl="~/internship/InternProfile.aspx" 
                Visible="False">Click to update your Information!</asp:HyperLink>--%>
            <asp:DropDownList ID="ddlinstitutionId" runat="server"  AppendDataBoundItems="True" TabIndex="6" CssClass="myFormInputControls"
                OnSelectedIndexChanged="ddlinstitutionId_SelectedIndexChanged">
            </asp:DropDownList>
            <br />

            <asp:Label ID="lblFName" runat="server" Text="First Name" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtFName" runat="server" CssClass="myFormInputControls"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                ErrorMessage="*" ControlToValidate="txtFName"
                ForeColor="Red" ValidationGroup=""></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblMName" runat="server" Text="Middle Name" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtMName" runat="server" CssClass="myFormInputControls"></asp:TextBox>
<%--            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                ErrorMessage="*" ControlToValidate="txtMName"
                ForeColor="Red" ValidationGroup=""></asp:RequiredFieldValidator>--%>
            <br />
            <asp:Label ID="lblLName" runat="server" Text="Last Name" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtLName" runat="server" CssClass="myFormInputControls"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                ErrorMessage="*" ControlToValidate="txtLName"
                ForeColor="Red" ValidationGroup=""></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblCell" runat="server" Text="Cell" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtCell" runat="server" CssClass="myFormInputControls"></asp:TextBox>
<%--              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                ErrorMessage="*" ControlToValidate="txtCell"
                ForeColor="Red" ValidationGroup=""></asp:RequiredFieldValidator>--%>
            <br />
            <asp:Label ID="lblOfficePhone" runat="server" Text="Office Phone" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtOfficePhone" runat="server" CssClass="myFormInputControls"></asp:TextBox>
<%--              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                ErrorMessage="*" ControlToValidate="txtOfficePhone"
                ForeColor="Red" ValidationGroup=""></asp:RequiredFieldValidator>--%>
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="myFormInputControls"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                ErrorMessage="*" ControlToValidate="txtEmail"
                ForeColor="Red" ValidationGroup=""></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblNote" runat="server" Text="Note" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtNote" runat="server" CssClass="myFormInputControls my-txtNote" TextMode="MultiLine" TabIndex="9"></asp:TextBox><br />
                        <br />
                <asp:Button  ID="btnSave"  runat="server" Text="Submit" OnClick="btnSave_Click" Visible="true" CssClass="myBtnSubmit" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"  Visible="false" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                <asp:Button ID="btnShowAllInterns" runat="server" Text="Show Advisors"
                OnClick="btnShowAllInterns_Click" Visible="False" />
        </fieldset>
    </div>

<%--         <div>
             <asp:Button ID="btnExportToExcel" runat="server"
                 OnClick="btnExportToExcel_Click" Text="Export To Excel" />
             <asp:Button ID="btnExportToWord" runat="server" OnClick="btnExportToWord_Click"
                 Text="Export To Word" />
             <asp:ImageButton ID="ImageBtnExportToExcel" runat="server"
                 ImageUrl="~/images/ExportToExcel.png"
                 OnClick="ImageBtnExportToExcel_Click" />
             <asp:ImageButton ID="ImageBtnExportToWord" runat="server"
                 ImageUrl="~/images/ExportToWord.png" OnClick="ImageBtnExportToWord_Click" />
             <asp:ImageButton ID="ImageBtnExportToPdf" runat="server"
                 ImageUrl="~/images/ExportToPdf.png" OnClick="ImageBtnExportToPdf_Click" />

             <asp:ImageButton ID="ImageBtnExportToCsv" runat="server"
                 ImageUrl="~/images/ExportToCsv.png" OnClick="ImageBtnExportToCsv_Click" />

             <asp:Button ID="btnExportTextToPDF" runat="server"
                 OnClick="btnExportTextToPDF_Click" Text="ExportTextToPdf" />

             <asp:LinkButton ID="LBExportToExcelViaCL" runat="server"
                 OnClick="LBExportToExcelViaCL_Click">ExportToExcelViaCL</asp:LinkButton>
         </div>--%>
         <div class="myGv">
             <fieldset class="gvStyle">
                 <legend>I<strong>nstitution Advisor Information </strong></legend>  
                 <asp:GridView ID="gvInstitutionAdvisor" runat="server" AutoGenerateColumns="False"  Visible="false"
                     ShowHeaderWhenEmpty="True"
                     EmptyDataText="No Records Found" 
                     GridLines="both" CssClass="gv" 
                     EmptyDataRowStyle-ForeColor="Red"
                     AllowSorting="true"
                     OnSorting="GvEvent_Sorting"
                     AllowPaging="true"
                     OnPageIndexChanging="GridView1_PageIndexChanging"
                     PageSize="30">
                     <Columns>

                        <asp:TemplateField HeaderText="InstitutionId"  SortExpression ="institutionId" Visible="false">
                             <ItemTemplate>
                                 <asp:Label ID="lblInstitutionId" runat="server" Text='<%#Eval("InstitutionId") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Institution"  SortExpression ="Institution">
                             <ItemTemplate>
                                 <asp:Label ID="lblInstitiution" runat="server" Text='<%#Eval("Institution") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="First Name " SortExpression ="FName">
                             <ItemTemplate>
                                 <asp:Label ID="lblFName" runat="server" Text='<%#Eval("fName") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="MI"  SortExpression ="MI">
                             <ItemTemplate>
                                 <asp:Label ID="lblMi" runat="server" Text='<%#Eval("mi") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="Last Name"  SortExpression ="lName">
                             <ItemTemplate>
                                 <asp:Label ID="lblLName" runat="server" Text='<%#Eval("lName") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cell"  SortExpression ="cell">
                             <ItemTemplate>
                                 <asp:Label ID="lblCell" runat="server" Text='<%#Eval("cell") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Office Phone"  SortExpression ="officePhone">
                             <ItemTemplate>
                                 <asp:Label ID="lblOfficePhone" runat="server" Text='<%#Eval("officePhone") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Email"  SortExpression ="email">
                             <ItemTemplate>
                                 <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Note"  SortExpression ="note">
                             <ItemTemplate>
                                 <asp:Label ID="lblNote" runat="server" Text='<%#Eval("note") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>                         
                         <asp:TemplateField HeaderText="Action"  SortExpression ="action" Visible="false" >
                             <ItemTemplate>
                                 <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                                 <asp:Button ID="btnDelete" runat="server" Text="Delete"
                                     OnClientClick="return confirm('Are you sure? want to delete !');"
                                     OnClick="btnDelete_Click" />
                                 <asp:Label ID="lblInstitutionAdvisorId" runat="server" 
                                     Text='<%#Eval("institutionAdvisorId") %>' 
                                        Visible="false"></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                 </asp:GridView>
             </fieldset>
         </div>
        <input type="hidden" runat="server" id="hidinstitutionAdvisorId" />

</asp:Content>
