<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApartmentEditor.aspx.cs" Inherits="Admin.ApartmentEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <br />
        <div class="row">
            <div class="col-sm-5">
                <div class="form-group">
                    <label class="label-custom">Vlasnik</label>
                    <asp:DropDownList ID="ddlApartmentOwner" runat="server" CssClass="form-control font-goth" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label class="label-custom">Status</label>
                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control font-goth" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label class="label-custom">Naziv</label>
                    <asp:TextBox ID="tbName" runat="server" CssClass="form-control font-goth"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="label-custom">Adresa</label>
                    <asp:TextBox ID="tbAddress" runat="server" CssClass="form-control font-goth"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="label-custom">Grad</label>
                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control font-goth" DataValueField="Id" DataTextField="Name"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <label class="label-custom">Cijena</label>
                    <div class="input-group">
                        <span class="input-group-addon font-goth" id="sizing-addon1">HRK</span>
                        <asp:TextBox ID="tbPrice" runat="server" CssClass="form-control font-goth"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="label-custom">Broj odraslih</label>
                    <asp:TextBox ID="tbMaxAdults" runat="server" TextMode="Number" CssClass="form-control font-goth"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="label-custom">Broj djece</label>
                    <asp:TextBox ID="tbMaxChildren" runat="server" TextMode="Number" CssClass="form-control font-goth"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="label-custom">Broj soba</label>
                    <asp:TextBox ID="tbTotalRooms" runat="server" TextMode="Number" CssClass="form-control font-goth"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label class="label-custom">Udaljenost od plaže</label>
                    <asp:TextBox ID="tbBeachDistance" runat="server" TextMode="Number" CssClass="form-control font-goth"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-2"></div>

            <div class="col-sm-5">
                <div class="form-group">
                    <label class="label-custom">Odabir tagova</label>
                    <asp:DropDownList ID="ddlTags" runat="server" CssClass="form-control font-goth" DataValueField="Id" DataTextField="Name" OnSelectedIndexChanged="ddlTags_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-md-7">
                    <asp:Repeater ID="repTags" runat="server">
                        <HeaderTemplate>
                            <ul class="list-group">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="list-group-item">
                                <asp:HiddenField runat="server" ID="hidTagId" Value='<%# Eval("ID") %>' />
                                <asp:Label runat="server" ID="txtTagName" CssClass="font-goth" Text='<%# Eval("Name") %>' />
                                <asp:LinkButton runat="server" Style="float: right; padding: 1px" ID="btnDeleteTag" CssClass="btn" OnClick="btnDeleteTag_Click">
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
        <br />

        <div class="container-fluid">
            <div class="form-group text-center">
                <label class="btn btn-custom">
                    Učitaj sliku
                    <asp:FileUpload ID="uplImages" runat="server" CssClass="hidden" AllowMultiple="true" OnChange="handleFileSelect(this.files)" />
                </label>
                <hr style="width: 5px" />
                <div class="text-center" id="uplImageInfo"></div>
                <script>
                    function handleFileSelect(files) {
                        for (var i = 0; i < files.length; i++) {
                            $span = $("<span class='label label-info'></span>").text(files[i].name);
                            $('#uplImageInfo').append($span);
                            $('#uplImageInfo').append("<br />");
                        }
                    }
                </script>
            </div>
        </div>

        <div class="container-fluid">
            <asp:Repeater ID="repApartmentPictures" runat="server">
                <ItemTemplate>
                    <div class="col-sm-3">
                        <div class="form-group text-center">
                            <asp:HiddenField runat="server" ID="hidApartmentPictureId" Value='<%# Eval("ID") %>' />
                            <table class="thumbnail">
                                <tr>
                                    <td class="table-pictures" colspan="2">
                                        <a href="<%# Eval("Path") %>">
                                            <asp:Image ID="imgApartmentPicture" runat="server" CssClass="thumbnail size1of4" ImageUrl='<%# Eval("Path") %>' />
                                        </a>
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtApartmentPicture" runat="server" CssClass="form-control text-center" Text='<%# Eval("Name") %>' placeholder="Naziv" />
                                    </td>
                                </tr>

                                <tr>
                                    <td class="table-pictures" colspan="2"></td>
                                </tr>

                                <tr>
                                    <td class="table-pictures">
                                        <label class="btn btn-custom">
                                            <asp:CheckBox ID="cbIsRepresentative" runat="server" CssClass="is-representative-picture" Checked='<%# Eval("IsRepresentative") %>' />
                                            Glavna slika
                                        </label>
                                    </td>
                                    <td class="table-pictures">
                                        <label class="btn btn-danger">
                                            <asp:CheckBox ID="cbDelete" runat="server" Checked="false" />
                                            Obriši
                                        </label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <hr style="width: 5px" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <%--<asp:Repeater ID="repApartmentPictures" runat="server">
                <ItemTemplate>
                    <div class="form-group">
                        <div class="row">
                            <asp:HiddenField runat="server" ID="hidApartmentPictureId" Value='<%# Eval("ID") %>' />
                            <div class="col-md-3">
                                <a href="<%# Eval("Path") %>">
                                    <asp:Image ID="imgApartmentPicture" runat="server" CssClass="img-thumbnail" ImageUrl='<%# Eval("Path") %>' Width="320" Height="200" />
                                </a>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtApartmentPicture" runat="server" CssClass="form-control" Text='<%# Eval("Name") %>' placeholder="Naziv" />
                                </div>
                                <div class="form-group">
                                    <label class="btn btn-success">
                                        Glavna slika
                                        <asp:CheckBox ID="cbIsRepresentative" runat="server" CssClass="is-representative-picture" Checked='<%# Eval("IsRepresentative") %>' />
                                    </label>
                                </div>
                                <div class="form-group">
                                    <label class="btn btn-danger">
                                        <span class="glyphicon glyphicon-trash"></span>
                                        <asp:CheckBox ID="cbDelete" runat="server" Checked="false" />
                                    </label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <hr style="width: 5px" />
                </ItemTemplate>
            </asp:Repeater>--%>
            <script>
                $(function () {
                    var repPicCheckboxes = $(".is-representative-picture > input[type=checkbox]");
                    repPicCheckboxes.change(function () {
                        currentCheckbox = this;
                        if (currentCheckbox.checked) {
                            repPicCheckboxes.each(function () {
                                otherCheckbox = this
                                if (currentCheckbox != otherCheckbox && otherCheckbox.checked) {
                                    otherCheckbox.checked = false;
                                }
                            })
                        }
                    });
                })
            </script>
        </div>

        <hr style="width: 10px" />

        <div class="text-center">
            <asp:LinkButton ID="lblSave" runat="server" Title="Save" Text="Spremi" CssClass="btn" Font-Size="60px" OnClick="lblSave_Click">
                <span class="glyphicon glyphicon-floppy-save glyphicon-custom"></span>
            </asp:LinkButton>
            <asp:LinkButton ID="lbReturn" runat="server" Title="Cancel without saving" Text="Povratak" CssClass="btn" Font-Size="60px" OnClick="lbReturn_Click">
                <span class="glyphicon glyphicon-log-out glyphicon-custom"></span>
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
