<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentList.aspx.cs" Inherits="Admin.ApartmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="font-family: 'Century Gothic'">
        <br />

        <div class="col-sm-4">
            <asp:Label ID="lblStatus" CssClass="label-custom" runat="server" Text="Status:"></asp:Label>
            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" DataValueField="Id" DataTextField="Name" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="lblCity" CssClass="label-custom" runat="server" Text="Grad:"></asp:Label>
            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" DataValueField="Id" DataTextField="Name" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-4">
            <asp:Label ID="lblOrder" CssClass="label-custom" runat="server" Text="Sortiranje:"></asp:Label>
            <asp:DropDownList ID="ddlOrder" runat="server" CssClass="form-control" DataValueField="Id" DataTextField="Name" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>

        <hr style="width: 10px" />

        <asp:GridView ID="gvListaApartmana" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover">
            <Columns>
                <asp:BoundField DataField="OwnerName" HeaderText="Vlasnik" />
                <asp:BoundField DataField="StatusName" HeaderText="Status" />
                <asp:BoundField DataField="CityName" HeaderText="Grad" />
                <asp:BoundField DataField="Address" HeaderText="Adresa" />
                <asp:BoundField DataField="Name" HeaderText="Naziv" />
                <asp:BoundField DataField="MaxAdults" HeaderText="Broj odraslih" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="MaxChildren" HeaderText="Broj djece" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="TotalRooms" HeaderText="Broj soba" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="BeachDistance" HeaderText="Udaljenost od plaže" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="Price" HeaderText="Cijena" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" />
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlEditor" runat="server" Style="vertical-align:middle" CssClass="btn btn-xs" Title="Edit apartment" Text="Uredi" NavigateUrl='<%# Eval("Id", "ApartmentEditor.aspx?Id={0}") %>'>
                            <span class="glyphicon glyphicon-pencil glyphicon-custom"></span>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:HyperLink ID="hlDelete" runat="server" Style="vertical-align:middle" CssClass="btn btn-xs" Title="Delete apartment" NavigateUrl='<%# Eval("Id", "ApartmentDelete.aspx?Id={0}") %>'>
                            <span class="glyphicon glyphicon-trash glyphicon-custom"></span>
                        </asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <div class="text-center">
            <asp:LinkButton ID="lbApartmentEditor" runat="server" Text="Add apartment" CssClass="btn btn-custom" OnClick="lbApartmentEditor_Click" />
        </div>

    </div>
</asp:Content>
