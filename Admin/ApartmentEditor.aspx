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
                <div class="form-group">
                    <label>Odabir tagova</label>
                    <asp:DropDownList ID="ddlTags" runat="server" CssClass="form-control" DataValueField="Id" DataTextField="Name" OnSelectedIndexChanged="ddlTags_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-md-7">
                    <asp:Repeater ID="repTags" runat="server">
                        <HeaderTemplate>
                            <ul class="list-group">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="list-group-item">
                                <asp:HiddenField runat="server" ID="hidTagId" Value='<%# Eval("ID") %>' />
                                <asp:Label runat="server" ID="txtTagName" Text='<%# Eval("Name") %>' />
                                <asp:LinkButton runat="server" ID="btnDeleteTag" CssClass="btn" OnClick="btnDeleteTag_Click">
                                        <span class="glyphicon glyphicon-trash"></span>
                                </asp:LinkButton>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
                <div class="col-md-5">
                </div>
            </div>
        </div>
        <div class="text-center">
            <asp:LinkButton ID="lblSave" runat="server" Text="Spremi" CssClass="btn" Font-Size="60px" OnClick="lblSave_Click">
                <span class="glyphicon glyphicon-floppy-save"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbReturn" runat="server" Text="Povratak" CssClass="btn" Font-Size="60px" OnClick="lbReturn_Click">
                <span class="glyphicon glyphicon-log-out"></span>
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
