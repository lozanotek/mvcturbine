<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<WebTest.Models.Product>" %>
<%@ Import Namespace="FubuMvc.Blades.UI"%>
<%@ Import Namespace="WebTest.Models"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Product Details</h2>
    
		<%--<p><%= Html.LabelFor(x => x.Id) %>: <%= Html.InputFor(x => x.Id) %> <%= Html.DisplayFor(x => x.Id) %></p>--%>
		<p><%= Html.LabelFor(x => x.Name) %>: <%= Html.InputFor(x => x.Name) %> <%= Html.DisplayFor(x => x.Name) %></p>
		<p><%= Html.LabelFor(x => x.Created) %>: <%= Html.InputFor(x => x.Created)%> <%= Html.DisplayFor(x => x.Created)%></p>
		<p><%= Html.LabelFor(x => x.InStock) %>: <%= Html.InputFor(x => x.InStock)%> <%= Html.DisplayFor(x => x.InStock)%></p>

</asp:Content>
