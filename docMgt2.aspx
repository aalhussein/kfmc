<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="docMgt2.aspx.cs" Inherits="docMgt2" ValidateRequest = "false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table><tr><td>
  
        <table>
            <tr>
                <td>
                    <fieldset >
                        <legend><strong>Document Management</strong></legend>
                        <table border="0">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="lblOutput" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Folder Name</td>
                                <td>
                                    <asp:TextBox ID="txtFolderName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>File Name</td>
                                <td>
                                    <asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>Doc Key</td>
                                <td>
                                    <asp:TextBox ID="txtDocKey" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>Doc Type:</td>
                                <td>
                                    <asp:DropDownList ID="ddlDocType" runat="server"
                                        AppendDataBoundItems="True" 
                                       Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                   
                                    <br />
                                    <asp:TextBox ID="txtDocType" runat="server"></asp:TextBox><br />
                                      <asp:LinkButton ID="Lb" runat="server" 
                                       OnClick="Lb_Click">Add Doc Type</asp:LinkButton>

                                </td>
                            </tr>
                            <tr>
                                <td>Upload Date:</td>
                                <td>
                                    <asp:TextBox ID="TxtCalenderAction" runat="server"></asp:TextBox>
                                    <asp:Label ID="ADateMsg" runat="server"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>Note:</td>
                                <td>
                                    <asp:TextBox ID="txtnote" runat="server"
                                        Width="200px" TextMode="MultiLine"></asp:TextBox>
                                    <asp:Label ID="ActionMsg" runat="server"
                                        Text="Required" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>Documents:</td>
                                <td>
                                    <asp:FileUpload ID="FileUpload" runat="server" AllowMultiple="true" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="INSERT" OnClick="Insert" /></td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
        </table>
               </td><td style="vertical-align: top"  >
                   <div id ="searchDocs" runat="server"  visible="false" >
                        <fieldset class="InputForm">
                           <legend><strong>Search Docs</strong></legend>
                            <asp:TextBox ID="txtKeySearch" runat="server"></asp:TextBox><br />
                           <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" style="height: 26px" />
                            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" Visible="False" />
                       </fieldset>
                       </div>
                       </td></tr></table>
     <div>
        <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="False" Width="493px" 
                        OnSorting="GvEvent_Sorting" 
                        OnPageIndexChanging="gvDocs_PageIndexChanging"
                        PageSize="3" 
                      >
            <Columns>
<%--                <asp:TemplateField HeaderText="OfficeArchiveDocsid">
                    <ItemTemplate>                                              
                         <asp:Label ID="lbldocId" runat="server" Text='<%# Bind("OfficeArchiveDocsid") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
               <asp:TemplateField HeaderText="DocType ">
                    <ItemTemplate>
                        <asp:Label ID="lblDocType" runat="server" Text='<%# Bind("DocType") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FolderName">
                    <ItemTemplate>
                        <asp:Label ID="lblFolderName" runat="server" Text='<%# Bind("FolderName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="FileName">
                   <ItemTemplate>
                        <asp:Label ID="lblFileName" runat="server" Text='<%# Bind("FileName") %>'></asp:Label>
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="DocKey">
                   <ItemTemplate>
                        <asp:Label ID="lblDocKey" runat="server" Text='<%# Bind("DocKey") %>'></asp:Label>
                   </ItemTemplate>
                   <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Uploaded date">
                    <ItemTemplate>
                        <asp:Label ID="lbluploadDate" runat="server" Text='<%# Bind("uploadDate","{0:d}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="docName">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                            CommandArgument='<%# Bind("OfficeArchiveDocsid") %>' Text='<%# Eval("docName")  %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Note">
                    <ItemTemplate>
                        <asp:Label ID="lblNote" runat="server" Text='<%# Bind("Note") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>

