<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Mixed View Engines
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        I'm a mixed controller and I'm using the WebForms ViewEngine!</h2>
    <%Html.RenderPartial("sparkpartial"); %>
    <%Html.RenderPartial("nvelocity"); %>
</asp:Content>
