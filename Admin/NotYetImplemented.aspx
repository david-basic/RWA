<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NotYetImplemented.aspx.cs" Inherits="Admin.NotYetImplemented" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/CustomStyles.css" rel="stylesheet" />
    <title>Not yet</title>
</head>
<body>
    <form runat="server">
        <hr style="width: 10px" />

        <div class="container">
            <div class="well" style="background: #12477360">
                <h2 class="text-center display-2" style="color: #124773; font-family: 'Century Gothic'"><strong>Feature not yet implemented!</strong></h2>
            </div>
        </div>

        <div class="text-center">
            <asp:LinkButton ID="lbReturn" runat="server" Title="Return HOME" Text="Povratak" CssClass="btn" Font-Size="60px" OnClick="lbReturn_Click">
                <span class="glyphicon glyphicon-home glyphicon-custom"></span>
            </asp:LinkButton>
        </div>
    </form>

</body>
</html>
