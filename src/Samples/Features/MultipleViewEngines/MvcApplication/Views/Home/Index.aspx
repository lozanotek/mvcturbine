<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    WebForm View Engine
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        This view is brought to you by the WebForms ViewEngine!</h2>
    <p>
        To learn more about MVC Turbine visit <a href="http://mvcturbine.codeplex.com" title="MVC Turbine">
            http://mvcturbine.codeplex.com</a>.
    </p>
    <p>
        To learn more about ASP.NET MVC visit <a href="http://asp.net/mvc" title="ASP.NET MVC Website">
            http://asp.net/mvc</a>.
    </p>
</asp:Content>
