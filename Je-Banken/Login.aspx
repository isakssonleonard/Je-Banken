<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Je_Banken.Login" %>
<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">    
    <script type="text/javascript">
          $(document).ready(function () {
              $('#anstalld').hover(function () {
                  $('#anstalld').css("border", "1px solid #4d88ff");
              });

              $('#anstalld').mouseleave(function () {
                  $('#anstalld').css("border", "1px solid grey");
              });

              $('#Administrator').hover(function () {
                  $('#Administrator').css("border", "1px solid #4d88ff");
              });

              $('#Administrator').mouseleave(function () {
                  $('#Administrator').css("border", "1px solid grey");
              });
        });
    </script>    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <div class="container">
            <h2>Logga in här</h2>
            <asp:Button ID="anstalld" runat="server" Text="Anställd" CssClass="knapp" CommandArgument="1" OnCommand="anstalld_Command" ClientIDMode="Static" Width="137"/>
            <asp:Button ID="Administrator" runat="server" Text="Administratör" OnClick="Administrator_Click" CssClass="knapp knapp-lower" ClientIDMode="Static"/>
        </div>
    </div>
</asp:Content>
