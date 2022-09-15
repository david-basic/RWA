<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TagList.aspx.cs" Inherits="Admin.TagList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <hr style="width: 10px" />

    <div class="container">
        <div class="well" style="background: #12477360">
            <h2 class="text-center display-2" style="color: #124773; font-family: 'Century Gothic'"><strong>Feature not yet implemented!</strong></h2>
        </div>
    </div>

    <asp:GridView ID="gvListaTagova" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Tag" />
            <asp:BoundField DataField="NameEng" HeaderText="Tag na engleskom" />
            <asp:BoundField DataField="Name" HeaderText="Tip taga" />
            <asp:BoundField DataField="NameEng" HeaderText="Tip taga na engleskom" />
        </Columns>
    </asp:GridView>

</asp:Content>
