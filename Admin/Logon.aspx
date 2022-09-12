<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="Admin.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Logon</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <br />
    <div class="container col-sm-6 col-sm-offset-3" style="font-family:'Century Gothic'">
        <div class="well" style="background:#12477360">
            <h2 class="text-center display-2" style="color:#124773"><strong>Login page</strong></h2>
            <br />
            <form id="form1" class="form-horizontal" runat="server">
                <div class="form-group">
                    <label class="control-label col-sm-3" style="text-align:right; color:#124773" for="txtUserName">Email address:</label>
                    <div class="col-sm-7">
                        <input type="text" id="txtUserName" placeholder="Enter email" class="form-control" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="txtUserName"
                            Display="Static" ErrorMessage="* Username not valid" ForeColor="Red" runat="server"
                            ID="vUserName" />
                    </div>
                </div>
                
                <div class="form-group">
                    <label class="control-label col-sm-3" style="text-align:right; color:#124773" for="txtUserPass">Password:</label>
                    <div class="col-sm-7">
                        <input type="password" id="txtUserPass" placeholder="Enter password" class="form-control" runat="server" />
                        <asp:RequiredFieldValidator ControlToValidate="txtUserPass"
                            Display="Static" ErrorMessage="* Password not valid" ForeColor="Red" runat="server"
                            ID="vUserPass" />
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-7">
                        <asp:CheckBox ID="chkPersistCookie" runat="server" AutoPostBack="false" />
                        <label style="color:#124773; vertical-align:middle">Remember me</label>
                    </div>
                </div>
                <br />
                <div class="form-group">
                    <div class="text-center">
                        <asp:LinkButton ID="lbLogin" CssClass="btn btn-default" Style="background:#124773; color:white" OnClick="lbLogin_Click" runat="server"><strong>Login</strong></asp:LinkButton>
                        <asp:Label ID="lblMsg" ForeColor="red" Font-Name="Verdana" Font-Size="10" runat="server" />
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
