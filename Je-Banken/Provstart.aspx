<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Provstart.aspx.cs" Inherits="Je_Banken.ValAvTest" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">    
    <script type="text/javascript">
          $(document).ready(function () {
              $('#prov_knapp').hover(function () {
                  $('#prov_knapp').css("border", "1px solid #4d88ff");
              });

              $('#prov_knapp').mouseleave(function () {
                  $('#prov_knapp').css("border", "1px solid grey");
              });
        });
    </script>    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div class="container">
            <asp:Label ID="title" runat="server" Text="" CssClass="title-start"></asp:Label>
            <asp:Button ID="prov_knapp" runat="server" Text="Starta prov" CssClass="knapp provknapp" OnClick="prov_knapp_Click" ClientIDMode="Static" />
        </div>
    </div>
</asp:Content>
