<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="docMgt.aspx.cs" Inherits="kfmc.office.docMgt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%--    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
       <div class="myContentxx">

           <fieldset class="InputForm">
               <legend><strong>Document Management Entry</strong></legend>
               <asp:Label ID="lblOutput" runat="server" Text="" CssClass="myFormOutput"></asp:Label> <br /><br />
               <asp:Label ID="lblFolderName1" runat="server" Text="Folder Name:" CssClass="myFormCaption"></asp:Label>
               <asp:TextBox ID="txtFolderName" runat="server" CssClass="myFormInputControls"></asp:TextBox><br />

               <asp:Label ID="lblFileName" runat="server" Text="File Name:" CssClass="myFormCaption"></asp:Label>
               <asp:TextBox ID="txtFileName" runat="server" CssClass="myFormInputControls"></asp:TextBox>
               <br />

               <asp:Label ID="lblDocKey" runat="server" Text="Doc Key :" CssClass="myFormCaption"></asp:Label>
               <asp:TextBox ID="txtDocKey" runat="server" CssClass="myFormInputControls"></asp:TextBox><br />

                 <asp:Label ID="lblDocType" runat="server" Text="Doc Type:" CssClass="myFormCaption"></asp:Label>
               <asp:DropDownList ID="ddlDocType" runat="server" AppendDataBoundItems="True" Width="200px" CssClass="myFormInputControls">
               </asp:DropDownList>
   
      
             &nbsp;&nbsp; <asp:TextBox ID="txtDocType" runat="server" CssClass="myFormInputControls"></asp:TextBox>
               <asp:LinkButton ID="Lb" runat="server" OnClick="Lb_Click" CssClass="myFormInputControls">Add Doc Type</asp:LinkButton><br />

               <asp:Label ID="lblUploadDate" runat="server" Text="Upload Date:" CssClass="myFormCaption"></asp:Label>
               <asp:TextBox ID="TxtCalenderAction" runat="server" CssClass="myFormInputControls"></asp:TextBox>
               <br />
     
               <asp:Label ID="ADateMsg" runat="server"></asp:Label>
               <asp:Label ID="lblNote" runat="server" Text="Note:" CssClass="myFormCaption"></asp:Label>
               <asp:TextBox ID="txtnote" runat="server" Width="200px" TextMode="MultiLine" CssClass="myFormInputControls"></asp:TextBox>
               <br />
               <asp:Label ID="ActionMsg" runat="server" Text="Required" Visible="False"></asp:Label>

               <asp:Label ID="lblDocuments" runat="server" Text="Documents:" CssClass="myFormCaption"></asp:Label>
               <asp:FileUpload ID="FileUpload" runat="server" AllowMultiple="true" />
               <br />

               <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
               <asp:Button ID="btnShowDocs" runat="server" Text="Show Docs" OnClick="btnShowDocs_Click" Visible="False" />
           </fieldset>
      
               <fieldset class="InputForm" >
                   <legend><strong>Search Docs</strong></legend>

                   <asp:Label ID="lblFolderName2" runat="server" Text="Folder Name:" CssClass="myFormCaption"></asp:Label>
                   <asp:TextBox ID="txtFolderNameSearch" runat="server" CssClass="myFormInputControls"></asp:TextBox>
   
                   <asp:Label ID="lbltxtKeySearch" runat="server" Text="Keyword:" CssClass="myFormCaption"></asp:Label>
                   <asp:TextBox ID="txtKeySearch" runat="server" CssClass="myFormInputControls"></asp:TextBox>
                   <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" Visible="False" CssClass="myFormInputControls" />
                   <br />
               </fieldset>
 
  
     <div>
        <fieldset class="gvStyle">
            <legend ><strong>Document Management View </strong></legend>

        <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="False" 
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
               <asp:TemplateField HeaderText="DocType">
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
            </fieldset>
    </div>
          </div>

    
           <div id="searchDocs" runat="server" visible="false" CssClass="myFormInputControls" >          </div>
</asp:Content>
