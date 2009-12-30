<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Injectable filters</h2>
    <h3>
        <%= Html.Encode(ViewData["ActionMessage"]) %></h3>
    <h3>
        <%= Html.Encode(ViewData["AuthMessage"]) %></h3>
</asp:Content>
