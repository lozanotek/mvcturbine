<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Custom Blade Output</h2>
    <h3><%= Html.Encode(ViewData["BeforeDependency"]) %></h3>
    <h3><%= Html.Encode(ViewData["AfterDependency"])%></h3>
</asp:Content>
