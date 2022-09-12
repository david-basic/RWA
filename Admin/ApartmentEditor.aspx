<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentEditor.aspx.cs" Inherits="Admin.ApartmentEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <br />
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Vlasnik</label>
                    <asp:DropDownList ID="ddlApartmentOwner" runat="server" CssClass="form-control" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Naziv</label>
                    <asp:TextBox ID="tbName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Adresa</label>
                    <asp:TextBox ID="tbAddress" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Grad</label>
                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Cijena</label>
                    <div class="input-group">
                        <span class="input-group-addon" id="sizing-addon1">€</span>
                        <asp:TextBox ID="tbPrice" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label>Broj odraslih</label>
                    <asp:TextBox ID="tbMaxAdults" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Broj djece</label>
                    <asp:TextBox ID="tbMaxChildren" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Broj soba</label>
                    <asp:TextBox ID="tbTotalRooms" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Udaljenost od plaže</label>
                    <asp:TextBox ID="tbBeachDistance" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6">
                <!-- TU IDE ODABIR TAGOVA -->
            </div>
        </div>
    </div>
</asp:Content>
