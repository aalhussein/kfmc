<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="popUpCourse.aspx.cs" Inherits="kfmc.office.popUpCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Course</title>
        <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="myContent">
            <legend><strong>Add New Course </strong></legend>
            <asp:Label ID="lblOutput" runat="server"></asp:Label><br />

            <asp:Label ID="lblCourse" runat="server" Text="Course Name:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtCourse" runat="server" TabIndex="100" CssClass="myFormInputControls"></asp:TextBox>
            <br />

            <asp:Label ID="lblStartDate" runat="server" Text="start Date:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server"  TabIndex="101" CssClass=""></asp:TextBox>
            <br />

            <asp:Label ID="lblEndDate" runat="server" Text="End Date:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtEndDate" runat="server" CssClass="" TabIndex="102"></asp:TextBox>
            <br />

            <asp:Label ID="lblVenue" runat="server" Text="Venue:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtVenue" runat="server" CssClass="myFormInputControls" TabIndex="103"></asp:TextBox>
            <br />

            <asp:Label ID="lblactive" runat="server" Text="Active:" CssClass="myFormCaption"></asp:Label>
            <asp:CheckBox ID="cbActive" runat="server" TabIndex="104" CssClass="myFormInputControls"></asp:CheckBox>
            <br />

            <asp:Label ID="lblNote" runat="server" Text="Note:" CssClass="myFormCaption"></asp:Label>
            <asp:TextBox ID="txtNote" runat="server" Height="50px" TextMode="MultiLine" Width="251px" CssClass="myFormInputControls" TabIndex="105"></asp:TextBox>
            <br />

            <asp:Label ID="lblFileAttachments" runat="server" Text="File Attachments:" CssClass="myFormCaption"></asp:Label>
            <asp:FileUpload ID="FileUpload" runat="server" Enabled="true"
                AllowMultiple="true" Width="253px" TabIndex="2" CssClass="myFormInputControls" Height="20px"  /><br />

            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" TabIndex="106" />
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
                Visible="false" />
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />

</div>
        <asp:HiddenField ID="hdnCourseId" runat="server" />
    </form>
</body>
</html>
