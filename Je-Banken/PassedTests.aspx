<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PassedTests.aspx.cs" Inherits="Je_Banken.PassedTests" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        tr:nth-child(even){background-color: #e8e4e4}
        tr:nth-child(odd){background-color: #fff}
        .tidgare_prov_container tr:nth-child(odd), .tidgare_prov_container tr:nth-child(even){background-color: #eee}
        label {
            font-weight: normal !important;            
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="jumbotron">
        <h2 id="tidigare_prov_text" runat="server">Här kan du se ditt senaste prov</h2>
       
 <div class="table-responsive">          
  <table id="table" class="table table-hover table-bordered" style=" width:70%; margin:30px auto; text-align:left" runat="server">
      <tr>
        <td>Förnamn</td>
        <td id="firstName" runat="server"></td>
      </tr>
      <tr>
        <td>Efternamn</td>
        <td id="lastName" runat="server"></td>
      </tr>
      <tr>
        <td>Datum</td>
        <td id="time" runat="server"></td>
      </tr>
      <tr>
        <td>Totalpoäng</td>
        <td id="totalPoäng" runat="server"></td>
      </tr>
      <tr>
        <td>Poäng inom delområdet etik</td>
        <td id="totalPoängEtik" runat="server"></td>
      </tr>
      <tr>
        <td>Poäng inom delområdet ekonomi</td>
        <td id="totalPoängEkonomi" runat="server"></td>
      </tr>
      <tr>
        <td>Poäng inom delområdet produkter</td>
        <td id="totalPoängProdukter" runat="server"></td>
      </tr>
      <tr>
        <td>Status</td>
        <td id="godkänd" runat="server"></td>
      </tr>
      <tr>
        <td>Provtyp</td>
        <td id="provTyp" runat="server"></td>
      </tr>
  </table>
  </div>
    <div class="tidgare_prov_container list-style-pass-test">
        <div id="tidigare_prov_box" runat="server"></div>
    </div>
</div>
</asp:Content>