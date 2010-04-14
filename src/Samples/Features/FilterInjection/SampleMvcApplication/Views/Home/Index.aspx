<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Injectable filters</h2>
    <ul>
        <li>Injectable Action Filter: <strong><%= Html.Encode(ViewData["ActionMessage"]) %></strong></li>
        <li>Injectable Authorization Filter: <strong><%= Html.Encode(ViewData["AuthMessage"]) %></strong></li>
        <li>Regular Authorization Filter: <strong><%= Html.Encode(ViewData["ReplayAuthMessage"]) %></strong></li>
        <li>Regular Action Filter: <strong><%= Html.Encode(ViewData["ReplayActionMessage"]) %></strong></li>
        <li>Global Action Filter: <strong><%= Html.Encode(ViewData["GlobalActionMessage"])%></strong></li>
    </ul>
</asp:Content>
