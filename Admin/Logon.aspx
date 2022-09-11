<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Admin.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logon</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <div class="h3 text-center">
        <font face="Verdana">Login page</font>
    </div>
    <div class="container col-md-4 col-md-offset-5">
        <form id="form1" class="col-lg-4" runat="server">
            <div class="form-inline">
                <label for="txtUserName">Email address:</label>
                <input type="text" id="txtUserName" class="form-control" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtUserName"
                    Display="Static" ErrorMessage="* Username not valid" ForeColor="Red" runat="server"
                    ID="vUserName" />
            </div>
            <div class="form-inline">
                <label for="txtUserPass">Password:</label>
                <input type="password" id="txtUserPass" class="form-control" runat="server" />
                <asp:RequiredFieldValidator ControlToValidate="txtUserPass"
                    Display="Static" ErrorMessage="* Password not valid" ForeColor="Red" runat="server"
                    ID="vUserPass" />
            </div>
            <div class="form-inline">
                <label>Remember me:</label>
                <asp:CheckBox ID="chkPersistCookie" runat="server" AutoPostBack="false" />
            </div>
            <asp:LinkButton ID="lbLogin" CssClass="btn btn-default" OnClick="lbLogin_Click" runat="server">Login</asp:LinkButton>
            <asp:Label ID="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
        </form>
    </div>
</body>
</html>
