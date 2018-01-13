<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administrator.aspx.cs" Inherits="Je_Banken.Admin" %>
<asp:Content ID="scriptheader" ContentPlaceHolderID="head" runat="server">
     <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script>
        $(function () {
            $('[id*=GridView]').footable();
        });
   </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div class="container">
            <asp:LinkButton ID="admin_knapp1" runat="server" Text="Ska skriva prov" CssClass="admin-knappar" Font-Underline="false" OnClick="admin_knapp1_Click"></asp:LinkButton>
            <asp:LinkButton ID="admin_knapp2" runat="server" Text="Provtillfället" CssClass="admin-knappar" Font-Underline="false" OnClick="admin_knapp2_Click"></asp:LinkButton>
            <asp:LinkButton ID="admin_knapp3" runat="server" Text="Provresultatet" CssClass="admin-knappar" Font-Underline="false" OnClick="admin_knapp3_Click"></asp:LinkButton>
            <asp:LinkButton ID="admin_knapp4" runat="server" Text="Visa resultat" CssClass="admin-knappar" Font-Underline="false" OnClick="admin_knapp4_Click"></asp:LinkButton>                
                <asp:GridView ID="GridView"                      
                    runat="server"              
                    CssClass="footable table-hover table-bordered" BackColor="White" BorderStyle="Solid" BorderColor="#999999" style="margin-top: 50px;">              
                </asp:GridView>
             </div>
        </div>
</asp:Content>
