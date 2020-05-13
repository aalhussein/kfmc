<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="internProfile.aspx.cs"
    Inherits="kfmc.internship.internProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }

        .auto-style1 {
            width: 157px;
        }

        .auto-style2 {
            height: 26px;
            width: 157px;
            text-align: right;
        }

        .auto-style3 {
            width: 331px;
        }

        .auto-style4 {
            height: 26px;
            width: 331px;
        }

        .auto-style6 {
            border: 0px solid black;
            background-color: lightgray;
            width: 650px;
        }

        .auto-style7 {
            width: 157px;
            text-align: right;
        }

        .auto-style8 {
            width: 198px;
        }

        .auto-style9 {
            height: 26px;
            width: 198px;
        }

        .auto-style10 {
            height: 73px;
            width: 289px;
        }
    </style>
    <div>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
            CancelControlID="btnCloseDoc"
            TargetControlID="lblInternDoc"
            PopupControlID="Panel1">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="Panel1" runat="server" CssClass="Popup" align="center" Style="display: none">
            <iframe style="width: 400px; height: 280px; background-color: white;" id="Iframe1"
                scrolling="yes" src="~/internship/popUpDoc.aspx" runat="server"></iframe>
            <br />
            <asp:Button ID="btnCloseDoc" runat="server" Text="Close" />
        </asp:Panel>
    </div>

    <%--  
    <div>
        <asp:LinkButton ID="LinkButton1" runat="server">LinkButton</asp:LinkButton>
        <asp:Button ID="btnInternAdvisor" runat="server" Text="Intern Advisor" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
            CancelControlID="btnCloseInternAdvisor"
            TargetControlID="btnInternAdvisor"
            PopupControlID="Panel3">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="Panel3" runat="server" CssClass="Popup" align="center" Style="display: none">
            <iframe style="width: 380px; height: 240px; background-color: white;" id="Iframe3"
                scrolling="no" src="~/demo/internAdvisor.aspx" runat="server"></iframe>  
            <br />
            <asp:Button ID="btnCloseInternAdvisor" runat="server" Text="Close" />
        </asp:Panel>
    </div>--%>

    <%--<div>
    <asp:Button ID="btnShowPopup" runat="server" Text="ShowPopup" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender4" runat="server"
        TargetControlID="btnShowPopup"
        PopupControlID="pnlpopup"
        CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="150px" Width="400px" Style="display: none">
    </asp:Panel>
</div>--%>

    <div>
        <fieldset class="InputForm">
            <legend><strong>Intern Registration & Profile</strong></legend>
            <table border="0" class="auto-style6">
                <tr>
                    <td colspan="3">
                        <asp:Label ID="lblOutput" runat="server"></asp:Label>
                        <asp:HyperLink ID="HyperLinkNextPage" runat="server" Visible="false"></asp:HyperLink>
                        <asp:HyperLink ID="myLink" runat="server" Visible="False"></asp:HyperLink><br />

                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">
                        <asp:Label ID="lblEnterYourEmail" runat="server" Text="Enter your Email" Visible="false"></asp:Label>
                    </td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtEmailSearch" runat="server" Width="209px" CssClass="auto-style22" Visible="false"></asp:TextBox>
                    </td>
                    <td class="auto-style3">
                        <asp:Button ID="btnGetInternProfile" runat="server" OnClick="btnGetInternProfile_Click" Text="Get Profile" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">Ref #</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtRefNo" runat="server" Height="16px" OnLoad="txtRefNo_Load"
                            ToolTip="If you have ref#, please enter here, If not leave it blank"
                            Width="40px" TabIndex="1" Enabled="False"></asp:TextBox>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Intern Status</td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddInternStatus" runat="server" AppendDataBoundItems="True" TabIndex="2" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Profile Group</td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddIInternGroup" runat="server" TabIndex="3" CssClass="myFormInputControls" Enabled="true">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3" style="vertical-align: middle;">
                        <asp:TextBox ID="txtInternGroup" runat="server" Visible="False" OnUnload="txtInternGroup_Unload"></asp:TextBox>
                        <asp:LinkButton ID="LbInternGroup" runat="server"
                            OnClick="LbAddInternGroup_Click" Visible="False">Add Intern Group</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">Administration</td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddIAdministration" runat="server" TabIndex="4" CssClass="myFormInputControls" Enabled="False">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">First Name</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtFName" runat="server" Width="209px" TabIndex="5"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="*" ControlToValidate="txtFName"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Middle Name</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtMName" runat="server" Width="209px" TabIndex="6"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="*" ControlToValidate="txtMName"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Last Name</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtLastName" runat="server" Width="209px" TabIndex="7"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="*" ControlToValidate="txtLastName"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Gender</td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddlGender" runat="server" TabIndex="8">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Cell Phone</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtCell" runat="server"
                            Width="209px" MaxLength="10" ToolTip="Number Only,No area code" TabIndex="9"></asp:TextBox>
                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1"
                            runat="server" TargetControlID="txtCell" Mask="9999999999"
                            MaskType="Number" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ErrorMessage="*" ControlToValidate="txtCell"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style3"></td>
                </tr>
                <tr>
                    <td class="auto-style7">Email </td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtEmail" runat="server" Width="209px" TabIndex="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ErrorMessage="*" ControlToValidate="txtEmail"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style3">&nbsp;</td>

                </tr>
                <tr>
                    <td class="auto-style7">Institution&nbsp; </td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddlinstitution" runat="server"
                            AppendDataBoundItems="True"
                            Width="209px" TabIndex="11">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3" style="vertical-align: middle;">
                        <asp:TextBox ID="txtInstitution" runat="server" Visible="False"></asp:TextBox>
                        <asp:LinkButton ID="LbAddInstitution" runat="server"
                            OnClick="LbAddInstitution_Click" Visible="False">Add Institution</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7">GPA</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtGpa" runat="server" Width="64px" MaxLength="4" TabIndex="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvGpa" runat="server"
                            ErrorMessage="*" ControlToValidate="txtGPA"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Degree</td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddlDegree" runat="server" AppendDataBoundItems="True" Width="209px" TabIndex="13">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Major</td>
                    <td class="auto-style8">
                        <asp:DropDownList ID="ddlDegreeMajor" runat="server" AppendDataBoundItems="True" Width="209px" TabIndex="14">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Training Term</td>
                    <td class="auto-style8">

                        <asp:DropDownList ID="ddlTrainingTerm" runat="server" AppendDataBoundItems="True" Enabled="true" TabIndex="15">
                        </asp:DropDownList>

                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">Training Year</td>
                    <td class="auto-style9">

                        <asp:DropDownList ID="ddlTrainingYear" runat="server" AppendDataBoundItems="True" TabIndex="16">
                        </asp:DropDownList>

                    </td>
                    <td class="auto-style4"></td>
                </tr>
                <tr>
                    <td class="auto-style7">Training Hours</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtTrainingHours" runat="server" Height="16px" Width="70px" TabIndex="17"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Supervisor Name</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtSupervisorName" runat="server" Height="16px" Width="209px" TabIndex="17"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Supervisor Cell </td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtSupervisorCell" runat="server" Height="16px" Width="209px" TabIndex="17"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7">Supervisor Email</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtSupervisorEmail" runat="server" Height="16px" Width="209px" TabIndex="17"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr hidden="hidden">
                    <td class="auto-style7">Registration Date</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtRegistrationDate" runat="server" ReadOnly="True" TabIndex="18" Visible="true"></asp:TextBox>
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style7" style="border-style: solid;">Note</td>
                    <td colspan="2">
                        <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine"
                            Width="342px" TabIndex="19" Height="75px"></asp:TextBox>
                    </td>

                </tr>
                <%--                            <tr>
                    <td class="">Date:</td>
                    <td>
                        <asp:TextBox
                            ID="TxtCalenderAction" runat="server"
                            Width="174px"></asp:TextBox>
                    </td>
                    <td class="">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="">profileComplete</td>
                    <td>
                        <asp:CheckBox ID="cbActive" runat="server" />
                    </td>
                    <td class="">
                        &nbsp;</td>
                </tr>--%>
                <tr>
                    <td class="auto-style1">profileComplete:</td>
                    <td class="auto-style8">
                        <asp:CheckBox ID="cbProfileComplete" runat="server" TabIndex="20" />
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">Please upload the following Documents:
                    </td>
                    <td class="auto-style3">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style10" colspan="2">
                        <ol>
                            <li>AR - Academic Record </li>
                            <li>AS - Attendance Sheet</li>
                            <li>CV - Curriculum Vitae</li>
                            <li>ER - Evaluation Report</li>
                            <li>SC - Supervisor Contact</li>
                            <li>UL - University Letter</li>
                        </ol>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">&nbsp;</td>
                    <td class="auto-style8">
                        <asp:FileUpload ID="FileUpload" runat="server" AllowMultiple="true"
                            ToolTip="Please enter CV, Adacemic record and University Letter" TabIndex="21" Enabled="true" />
                    </td>
                    <td class="auto-style3">
       <asp:Button ID="btnInternDoc" runat="server" Text="Intern Doc" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnSubmit"
                            runat="server" Text="Submit"
                            OnClick="Insert" Width="111px" Visible="False" />
                        <asp:Button ID="btnShowAllInterns" runat="server" Text="Show All Interns"
                            OnClick="btnShowAllInterns_Click" Visible="true" Width="111px" />
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="111px" Visible="False" />
                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" Width="111px" />
                        <asp:Button ID="btnNotifyAdminOfRegistration" runat="server"
                            OnClick="btnNotifyAdminOfRegistration_Click" Text="Notify Intern" Style="height: 26px" Visible="False" Width="111px" />
                                <asp:Label ID="lblInternDoc" runat="server" Text="InternDoc" Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div class="myGv">
        <fieldset class="gvStyle">
            <legend class=""><strong>Student Uploaded&nbsp; Information</strong></legend>
            <asp:GridView ID="gvIntern" runat="server" AutoGenerateColumns="False" DataKeyNames="internId"
                ShowHeaderWhenEmpty="True"
                EmptyDataText="No Records Found"
                GridLines="both" CssClass="gv"
                EmptyDataRowStyle-ForeColor="Red"
                AllowSorting="true"
                OnSorting="gvIntern_Sorting"
                AllowPaging="true"
                OnPageIndexChanging="gvIntern_PageIndexChanging"
                PageSize="50" OnRowDataBound="gvIntern_RowDataBound">

                <Columns>
                    <%--                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Intern Doc">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server"
                                CommandArgument='<%# Bind("internId") %>'
                                Text='<%# Eval("internId")  %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left"></ItemStyle>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="Intern Docs">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtn" ImageUrl="~/images/docImg.png" runat="server" Width="50" Height="50"
                                OnClick="imgbtn_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DocName">
                        <ItemTemplate>
                            <asp:BulletedList ID="bLInternDoc" runat="server"
                                DataTextField="DocName"
                                DataValueField="InternDocId">
                            </asp:BulletedList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="internId" InsertVisible="False" SortExpression="internId">
                        <ItemTemplate>
                            <asp:Label ID="lblInternId" runat="server" Text='<%# Bind("internId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:BoundField DataField="UploadedDate" HeaderText="UploadedDate" SortExpression="UploadedDate" />--%>
                    <asp:TemplateField HeaderText="RefNo" SortExpression="RefNo">
                        <ItemTemplate>
                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("RefNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Administration" SortExpression="Administration" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAdministrationId" runat="server" Text='<%# Bind("AdministrationId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblAdministration" runat="server" Text='<%# Bind("Administration") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="InternGroup" SortExpression="InternGroup" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblInternGroupId" runat="server" Text='<%# Bind("InternGroupId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblInternGroup" runat="server" Text='<%# Bind("InternGroup") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="InternStatus" SortExpression="InternStatus">
                        <ItemTemplate>
                            <asp:Label ID="lblInternStatusId" runat="server" Text='<%# Bind("InternStatusId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblInternStatus" runat="server" Text='<%# Bind("InternStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Degree" SortExpression="Degree">
                        <ItemTemplate>
                            <asp:Label ID="lblDegreeId" runat="server" Text='<%# Bind("DegreeId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblDegree" runat="server" Text='<%# Bind("Degree") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DegreeMajor" SortExpression="DegreeMajor">
                        <ItemTemplate>
                            <asp:Label ID="lblDegreeMajorId" runat="server" Text='<%# Bind("DegreeMajorId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblDegreeMajor" runat="server" Text='<%# Bind("Degree") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="fname" SortExpression="fname">
                        <ItemTemplate>
                            <asp:Label ID="lblFname" runat="server" Text='<%# Bind("fName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="mi" SortExpression="mi">
                        <ItemTemplate>
                            <asp:Label ID="lblMn" runat="server" Text='<%# Bind("mi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="lName" SortExpression="lName">
                        <ItemTemplate>
                            <asp:Label ID="lblLname" runat="server" Text='<%# Bind("lName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="gpa" SortExpression="gpa">
                        <ItemTemplate>
                            <asp:Label ID="lblGpa" runat="server" Text='<%# Bind("gpa") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="institution" SortExpression="institution">
                        <ItemTemplate>
                            <asp:Label ID="lblInstitutionId" runat="server" Text='<%# Bind("institutionId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblInstitution" runat="server" Text='<%# Bind("institution") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="cell" SortExpression="cell">
                        <ItemTemplate>
                            <asp:Label ID="lblCell" runat="server" Text='<%# Bind("cell") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="email" SortExpression="email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>' Visible="false"></asp:Label>
                            <asp:LinkButton ID="LbnInternDetail"
                                OnClick="sendEmail"
                                Text='<%# DataBinder.Eval(Container.DataItem,"email") %>'
                                CommandArgument='<%# Eval("email") %>'
                                CommandName="sendEmail"
                                ForeColor="#8C4510" runat="server">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="note" SortExpression="note" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblNote" runat="server" Text='<%# Bind("note") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="RegistrationDate" SortExpression="RegistrationDate" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRegistrationDate" runat="server" Text='<%# Bind("RegistrationDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Term" SortExpression="TrainingTerm">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingTermId" runat="server" Text='<%# Bind("TrainingTermId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblTrainingTerm" runat="server" Text='<%# Bind("TrainingTerm") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" SortExpression="TrainingYear">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingYearId" runat="server" Text='<%# Bind("TrainingYearId") %>' Visible="false"></asp:Label>
                            <asp:Label ID="lblTrainingYear" runat="server" Text='<%# Bind("TrainingYear") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hours" SortExpression="TrainingHours">
                        <ItemTemplate>
                            <asp:Label ID="lblTrainingHours" runat="server" Text='<%# Bind("TrainingHours") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Supervisor Name" SortExpression="supervisorName">
                        <ItemTemplate>
                            <asp:Label ID="lblSupervisorName" runat="server" Text='<%# Bind("supervisorName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supervisor Cell" SortExpression="supervisorCell">
                        <ItemTemplate>
                            <asp:Label ID="lblSupervisorCell" runat="server" Text='<%# Bind("supervisorCell") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supervisor Email" SortExpression="supervisorEmail">
                        <ItemTemplate>
                            <asp:Label ID="lblSupervisorEmail" runat="server" Text='<%# Bind("supervisorEmail") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="profileComplete" SortExpression="profileComplete" Visible="false">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxProfileComplete" runat="server" Checked='<%# Bind("profileComplete") %>' Enabled="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="110px">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                            <asp:Button ID="btnDelete" runat="server" Text="Delete"
                                OnClientClick="return confirm('Are you sure? want to delete the department.');"
                                OnClick="btnDelete_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </fieldset>
        <input type="hidden" runat="server" id="hidInternId" />
    </div>
    <%-- This is a popup to enter intern advisor --%>
</asp:Content>
