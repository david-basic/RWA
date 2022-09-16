<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TagList.aspx.cs" Inherits="Admin.TagList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <hr style="width: 10px" />

    <div class="container-fluid">
        <div class="col-sm-6 col-sm-offset-3">
            <asp:GridView ID="gvListaTagova" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Tag" />
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbTagEdit" runat="server" Title="Edit tag" CssClass="glyphicon glyphicon-pencil glyphicon-custom-small" OnClick="lbTagEdit_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbTagDelete" runat="server" Title="Delete tag" CssClass="glyphicon glyphicon-trash glyphicon-custom-small" OnClick="lbTagDelete_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="text-center">
        <asp:LinkButton ID="lblSave" runat="server" Title="Save" Text="Spremi" CssClass="btn" Font-Size="60px" OnClick="lblSave_Click">
                <span class="glyphicon glyphicon-floppy-save glyphicon-custom"></span>
        </asp:LinkButton>
        <asp:LinkButton ID="lbReturn" runat="server" Title="Cancel without saving" Text="Povratak" CssClass="btn" Font-Size="60px" OnClick="lbReturn_Click">
                <span class="glyphicon glyphicon-log-out glyphicon-custom"></span>
        </asp:LinkButton>
    </div>

</asp:Content>
