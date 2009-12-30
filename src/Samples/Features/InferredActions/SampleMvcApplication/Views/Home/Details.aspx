<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Details Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        More Details about Inferred Actions on MVC Turbine</h2>
    <p>
        There are no actions defined in this controller, so we'll let MVC Turbine infer
        them all. This means that MVC Turbine will automatically forward the request to
        the view.</p>
    <p>
        When the action is defined, it will then use the method within the controller class.</p>
</asp:Content>
