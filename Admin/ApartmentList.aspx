<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentList.aspx.cs" Inherits="Admin.ApartmentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="gvListaApartmana" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-condensed table-hover">
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
        </Columns>
    </asp:GridView>
</asp:Content>
