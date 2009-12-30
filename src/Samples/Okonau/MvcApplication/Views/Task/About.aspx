<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About</h2>
    <p>
        This is a sample application for<a href="http://turbineproject.com" target="_blank"> MVC Turbine</a>, 
        an open source project that converts the flow of your
        <a href="http://www.asp.net/mvc" target="_blank">ASP.NET MVC</a> application into useful work. 
        The features this sample shows are:</p>
    <ul>
        <li>Auto-registration of IHttpModules<ul>
            <li>Registers the NHibernateSessionModule</li>
            </ul>
        </li>
        <li>Use of multiple assemblies<ul>
            <li>Uses an MVC, Persistence assembly in conjuction with the main web application 
                assembly.</li>
            </ul>
        </li>
        <li>Registration of IServiceRegistrator<ul>
            <li>Registers repositories, domain services and other components to be used by the 
                application.</li>
            </ul>
        </li>
    </ul>
    <p>
        Feel free to look, modify or whatever with the code within this sample. If you 
        have any questions or comments, don&#39;t hesitate to <a href="http://bit.ly/5oa81O" target="_blank">
        start a conversation</a>!</p>
</asp:Content>
