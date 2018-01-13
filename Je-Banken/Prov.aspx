<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prov.aspx.cs" Inherits="Je_Banken.Prov" %>

<asp:Content ID="header" ContentPlaceHolderID="head" runat="server">   
    <script type="text/javascript">
          $(document).ready(function () {
              $(function () {
                  $(".info-box").hide().fadeOut(1500);
                  $(".info-box").hide().fadeIn(1500);
              });

              $('#submit_button').hover(function () {
                  $('#submit_button').css("border", "1px solid #4d88ff");
              });

              $('#submit_button').mouseleave(function () {
                  $('#submit_button').css("border", "1px solid grey");
              });             
        });
    </script>    
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div runat="server" id="info" class="alert alert-info info-box" style="margin-top: 35px" visible="false">
        <a class="close" data-dismiss="alert" aria-label="close">&times</a>
        <asp:Label ID="Label1" runat="server" Text="Du har inte svarat på alla frågor"></asp:Label> 
    </div>
    <div class="jumbotron">
       <div id="prov_div" runat="server">
       </div>
        <asp:Button ID="submit_button" runat="server" Text="Slutför testet" CssClass="=provknapp" OnClick="submit_button_Click" ClientIDMode="Static" />
    </div>
</asp:Content>


