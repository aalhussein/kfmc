<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="empTraining.aspx.cs" 
    Inherits="kfmc.employee.empTraining" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <%--<style type="text/css" >
        .auto-style17 {
            width: 107px;
        }
        .auto-style21 {
            width: 107px;
            height: 26px;
        }
        .auto-style22 {
            width: 226px;
            text-align: right;
        }
        </style>--%>
    <script lang="javascript" type="text/javascript">
    function validatePage() {
        //Executes all the validation controls associated with group1 validaiton Group1. To avoid double postback
        var flag = window.Page_ClientValidate('group1');
        if (flag)

        return flag;
    } 
</script>
           ................................................. Under Development ............................................
    <div class="myContent">
        <fieldset  class="InputForm">
            <legend><strong>Employee External Course Registration:</strong></legend>
            <table class="myTable">
                <tr>
                    <td colspan="2" class="caption">
                        <asp:Label ID="lblOutput" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Employee ID</td>
                    <td>
                        <asp:TextBox ID="txthitemployeeId" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">First Name</td>
                    <td>
                        <asp:TextBox ID="txtFName" runat="server" Width="200px" OnLoad="txtFName_Load"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                            ErrorMessage="*" ControlToValidate="txtFName"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Middle Name</td>
                    <td>
                        <asp:TextBox ID="txtMi" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="*" ControlToValidate="txtMi"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Last Name</td>
                    <td>
                        <asp:TextBox ID="txtlName" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="*" ControlToValidate="txtlName"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Ext</td>
                    <td>
                        <asp:TextBox ID="txtExt" runat="server" Width="64px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Department&nbsp; </td>
                    <td>
                        <asp:DropDownList ID="ddlhitDepartmentId" runat="server"
                            AppendDataBoundItems="True"
                            Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style9" style="text-align: left">
                        <%--                        <asp:TextBox ID="txtDepartment" runat="server"></asp:TextBox>
                                      <asp:LinkButton ID="Lb" runat="server" 
                                          OnClick="Lb_Click">Add Department</asp:LinkButton>--%>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style22">Position</td>
                    <td>
                        <asp:TextBox ID="txtPosition" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Cell Phone</td>
                    <td>
                        <asp:TextBox ID="txtCell" runat="server"
                            Width="174px" MaxLength="15" ToolTip="xxx-xxx-xxxxxx"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                            ErrorMessage="*" ControlToValidate="txtCell"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                        <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1"
    runat="server" TargetControlID="txtCell"  Mask="999-999-999999" 
    MaskType="Number" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" 
    OnInvalidCssClass="MaskedEditError" /> --%>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Email </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                            ErrorMessage="*" ControlToValidate="txtEmail"
                            ForeColor="Red" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Current Skills</td>
                    <td>
                        <asp:TextBox ID="txtCurrentSkills" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Target Skills</td>
                    <td>
                        <asp:TextBox ID="txtTgtSkills" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Course Name</td>
                    <td>
                        <asp:TextBox ID="txtCourseName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Start Date</td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">End Date</td>
                    <td>
                        <asp:TextBox
                            ID="txtEndDate" runat="server"
                            Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Cost </td>
                    <td>
                        <asp:TextBox ID="txtCost" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style21"></td>
                </tr>
                <tr>
                    <td class="auto-style22">Location</td>
                    <td>
                        <asp:TextBox ID="txtLocation" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Company Name</td>
                    <td>
                        <asp:TextBox ID="txtCompanyName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Company Phone</td>
                    <td>
                        <asp:TextBox ID="txtCompanyPhone" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Company Email</td>
                    <td>
                        <asp:TextBox ID="txtCompanyEmail" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">Company Rep</td>
                    <td>
                        <asp:TextBox ID="txtCompanyRep" runat="server" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <%--                            <tr>
                                <td class="auto-style22">Date:</td>
                                <td>
                                    <asp:TextBox
                                        ID="TxtCalenderAction" runat="server"
                                        Width="174px"></asp:TextBox>
                                </td>
                                <td class="auto-style21">
                                    </td>
                            </tr>--%>
                <tr>
                    <td class="auto-style22">active</td>
                    <td>
                        <asp:CheckBox ID="cbxActive" runat="server" />
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">Please upload all documents related&nbsp; to the course you want to register
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style22">&nbsp;</td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server" AllowMultiple="true"
                            ToolTip="Please enter CV, Adacemic record and University Letter" />
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSubmit"
                            runat="server" Text="Submit"
                            OnClick="Insert" />
                        <asp:Button ID="btnShowAllInterns" runat="server" Text="Show All Employees"
                            OnClick="btnShowAllInterns_Click" Visible="False" />
                        <asp:Button ID="btnAutoInsert" runat="server" OnClick="btnAutoInsert_Click" Text="AutoInsert" />
                    </td>
                    <td class="auto-style17">&nbsp;</td>
                </tr>
            </table>
        </fieldset>
        <br />
    </div>


    <div class="myGv">
        <fieldset class="gvStyle">
            <legend ><strong>Employee Uploaded&nbsp; Information</strong></legend>
            <asp:GridView ID="gvEmployeeDoc"  runat="server"  DataKeyNames="hitemployeeId" AutoGenerateColumns="False" CssClass="gv" 
                EmptyDataText="No Records Found" GridLines="both"  EmptyDataRowStyle-ForeColor="Red"      >
                <Columns>
                    <asp:BoundField DataField="hitemployeeId" HeaderText="hitemployeeId" InsertVisible="False" ReadOnly="True" SortExpression="hitemployeeId" />
                    <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                    <asp:BoundField DataField="fName" HeaderText="fName" SortExpression="fName" />
                    <asp:BoundField DataField="mi" HeaderText="mi" SortExpression="mi" />
                    <asp:BoundField DataField="lName" HeaderText="lName" SortExpression="lName" />
                    <asp:BoundField DataField="currentSkills" HeaderText="currentSkills" SortExpression="currentSkills" />
                    <asp:BoundField DataField="tgtSkills" HeaderText="tgtSkills" SortExpression="tgtSkills" />
                    <asp:BoundField DataField="courseName" HeaderText="courseName" SortExpression="courseName" />
                    <asp:BoundField DataField="startDate" HeaderText="startDate" SortExpression="startDate" />
                    <asp:BoundField DataField="endDate" HeaderText="endDate" SortExpression="endDate" />
                    <asp:BoundField DataField="cost" HeaderText="cost" SortExpression="cost" />
                    <asp:BoundField DataField="location" HeaderText="location" SortExpression="location" />
                    <asp:BoundField DataField="companyName" HeaderText="companyName" SortExpression="companyName" />
                    <asp:BoundField DataField="companyPhone" HeaderText="companyPhone" SortExpression="companyPhone" />
                    <asp:BoundField DataField="companyRep" HeaderText="companyRep" SortExpression="companyRep" />
                </Columns>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
