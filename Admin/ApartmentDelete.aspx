<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentDelete.aspx.cs" Inherits="Admin.ApartmentDelete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <hr style="width: 10px" />

    <div class="container-fluid">
            <div class="col-sm-3 col-sm-offset-2">
                <div class="form-group">
                    <label class="label-custom">Vlasnik</label>
                    <asp:Label ID="lblApartmentOwner" runat="server" CssClass="form-control font-goth"></asp:Label>
                </div>
                <div class="form-group">
                    <label class="label-custom">Naziv</label>
                    <asp:Label ID="lblName" runat="server" CssClass="form-control font-goth"></asp:Label>
                </div>
            </div>

            <div class="col-sm-3 col-sm-offset-2">
                <div class="form-group">
                    <label class="label-custom">Adresa</label>
                    <asp:Label ID="lblAddress" runat="server" CssClass="form-control font-goth"></asp:Label>
                </div>
                <div class="form-group">
                    <label class="label-custom">Grad</label>
                    <asp:Label ID="lblCity" runat="server" CssClass="form-control font-goth"></asp:Label>
                </div>
            </div>
    </div>

    <hr style="width:10px" />

    <div class="text-center">
        <asp:LinkButton ID="lbConfirmDelete" runat="server" Title="Delete apartment" Font-Size="60px" CssClass="btn" OnClick="lbConfirmDelete_Click">
            <span class="glyphicon glyphicon-erase glyphicon-custom"></span>
        </asp:LinkButton>
        <asp:LinkButton ID="lbBack" runat="server" Title="Cancel and return" Font-Size="60px" CssClass="btn" OnClick="lbBack_Click">
            <span class="glyphicon glyphicon-log-out glyphicon-custom"></span>
        </asp:LinkButton>
    </div>
</asp:Content>
