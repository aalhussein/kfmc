<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popUpDoc.aspx.cs" Inherits="kfmc.internship.popUpDoc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
       
        <div class="myGv">
            <fieldset class="gvStyle">
                    <asp:Label ID="lblOutput" runat="server" Text="" CssClass="myFormOutput"></asp:Label><br />
                <legend><strong>Intern Document</strong></legend>
                <asp:GridView ID="gvDocs" runat="server" AutoGenerateColumns="False"
                    PageSize="3">
                    <Columns>

                        <asp:TemplateField HeaderText="dateUploaded ">
                            <ItemTemplate>
                                <asp:Label ID="lblDateUploaded" runat="server" Text='<%# Bind("dateUploaded") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="docName">
                            <ItemTemplate>
                            <asp:LinkButton ID="lnkDownload" runat="server" OnClick="DownloadFile"
                                    CommandArgument='<%# Bind("InternDocId") %>' Text='<%# Eval("docName")  %>'></asp:LinkButton>    
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </fieldset>
        </div>
    </form>
</body>
</html>
