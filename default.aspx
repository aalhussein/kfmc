<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="kfmc.office._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="myContent">
        Office  Management page
        <br />
        <asp:HyperLink ID="hLDocMgt" runat="server" NavigateUrl="~/office/docMgt.aspx">Doc mgt</asp:HyperLink>
    </div>
</asp:Content>
