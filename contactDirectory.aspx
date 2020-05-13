<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="contactDirectory.aspx.cs"
    Inherits="kfmc.employee.contactDirectory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--    <link href="../style/gvStyle.css" rel="stylesheet" />--%>
    <div class="myContent">
        <fieldset class="InputForm">
            <legend><strong>Contact Information</strong></legend>
            <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Blue"></asp:Label><br />
            <table class="myTable">
                <tr>
                    <td colspan="2" style="text-align: left">
                        <asp:Label ID="lblOutput" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Administration </td>
                    <td>&nbsp;<asp:DropDownList ID="ddlAdministration" runat="server" Height="17px" Width="123px" AutoPostBack="True" OnSelectedIndexChanged="ddlAdministration_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Department
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" Width="123px" TabIndex="1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Contact Type</td>
                    <td>
                        <asp:DropDownList ID="ddlContactType" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Contact
                    </td>
                    <td>
                        <asp:TextBox ID="txtContact" runat="server" MaxLength="50" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Phone</td>
                    <td>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" TabIndex="4"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Email
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" TabIndex="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Active</td>
                    <td>
                        <asp:CheckBox ID="cbActive" runat="server" TabIndex="6" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">Note</td>
                    <td>
                        <asp:TextBox ID="txtNote" runat="server" Height="71px" TextMode="MultiLine" Width="251px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="7" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                            Visible="false" />
                        <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <div class="myGv">
            <fieldset class="gvStyle">
                <legend><strong>Contact Information </strong></legend>

                <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" CssClass="gv"
                    EmptyDataText="No Records Found" GridLines="both" EmptyDataRowStyle-ForeColor="Red">
                    <Columns>
                        <asp:BoundField DataField="contactId" HeaderText="contactId" SortExpression="" DataFormatString="" Visible="false" />

                        <asp:TemplateField HeaderText="contact">
                            <ItemTemplate>
                                <asp:Label ID="lblContactId" runat="server" Text='<%#Eval("contactId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblCustomer" runat="server" Text='<%#Eval("contact") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="AdministrationId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAdministrationId" runat="server" Text='<%#Eval("administrationId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Administration">
                            <ItemTemplate>
                                <asp:Label ID="lblAdministration" runat="server" Text='<%#Eval("administration") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DepartmentId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartmentId" runat="server" Text='<%#Eval("departmentId") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("department") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone Number">
                            <ItemTemplate>
                                <asp:Label ID="lblPhoneNumber" runat="server" Text='<%#Eval("PhoneNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <%-- <asp:Label ID="lblActive" runat="server" Text='<%#Eval("IsActive") %>'></asp:Label>--%>
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
        <input type="hidden" runat="server" id="hidCustomerID" />
    </div>
</asp:Content>
